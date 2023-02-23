using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Views
{
    public class ConstructionView : MonoBehaviour
    {
        public event Action<ElementView[]> OnCollisionHandler;

        private List<ElementView> _elements;
        private Transform _target;
        private Vector3 _force = Vector3.zero;
        private float _speed;

        public void Init(List<ElementView> elements, Transform target, float speed)
        {
            _elements = elements;
            _target = target;
            _speed = speed;

            foreach (var element in _elements)
            {
                element.Init(this);
                element.CollisionHandler += OnCollision;
            }
        }

        private void FixedUpdate()
        {
            Move();
            CollisionDetect();
        }

        private void OnDestroy()
        {
            _elements.ForEach(x => x.CollisionHandler -= OnCollision);
        }

        public void ChangeColliderState(bool value) =>
            _elements.ForEach(x => x.Collider.enabled = value);

        private void Move()
        {
            Vector3 transformPosition = transform.position;
            Vector3 targetPosition = _target.position;

            float forceTime = _speed / Vector3.Distance(_force, Vector3.zero) * Time.fixedDeltaTime;
            _force = Vector3.Lerp(_force, Vector3.zero, forceTime);

            Vector3 targetPoint = (targetPosition - _force);

            float distance = Vector3.Distance(transformPosition, targetPoint);
            float gravityTime = _speed / distance * Time.fixedDeltaTime;

            transform.position = Vector3.Lerp(transformPosition, targetPoint, gravityTime);
        }

        private void CollisionDetect() =>
            _elements.ForEach(x => x.CollisionDetect());

        private void OnCollision(ElementView[] elements) =>
            OnCollisionHandler?.Invoke(elements);
    }
}