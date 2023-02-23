using System;
using System.Collections.Generic;
using Code.Datas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Views
{
    public class ConstructionView : MonoBehaviour
    {
        public event Action<ElementView[]> OnCollisionHandler;

        private List<ElementView> _elements;
        private Transform _target;
        private float _speed;
        private ConstructionData _constructionData;
        private Vector3 _moveSpeed;
        private Vector3 _velocity;
        private bool _collidersIsActive;
        private Vector3 _rotation;

        public void Init(List<ElementView> elements, Transform target, float speed, ConstructionData constructionData)
        {
            _elements = elements;
            _target = target;
            _speed = speed;
            _constructionData = constructionData;

            _collidersIsActive = true;

            foreach (var element in _elements)
            {
                element.Init(this);
                element.CollisionHandler += OnCollision;
            }

            _rotation = Random.rotation.eulerAngles;
        }

        private void FixedUpdate()
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);
            if (_moveSpeed.magnitude < _constructionData.MaxForce / 2 &&
                !_collidersIsActive && distanceToTarget < _constructionData.DistanceForActivateCollider)
                ChangeColliderState(true);

            Rotation();
            Move();
            CollisionDetect();
        }

        private void OnDestroy()
        {
            _elements.ForEach(x => x.CollisionHandler -= OnCollision);
        }

        public void AddForce(Vector3 second)
        {
            Vector3 inNormal = second - transform.position;
            Vector3 velocity = _velocity == Vector3.zero ? -inNormal : _velocity;
            Vector3 reflect = Vector3.Reflect(velocity, inNormal).normalized;
            _moveSpeed = reflect * Random.Range(_constructionData.MinForce, _constructionData.MaxForce);
            _rotation = reflect + _moveSpeed.normalized;
        }

        public void ChangeColliderState(bool value)
        {
            _elements.ForEach(x => x.Collider.enabled = value);
            _collidersIsActive = value;
        }

        private void Move()
        {
            Vector3 transformPosition = transform.position;
            Vector3 targetPosition = _target.position;

            float forceTime = _speed / Vector3.Distance(_moveSpeed, Vector3.zero) * Time.fixedDeltaTime;
            _moveSpeed = Vector3.Lerp(_moveSpeed, Vector3.zero, forceTime);

            Vector3 targetPoint = (targetPosition - _moveSpeed);

            float distance = Vector3.Distance(transformPosition, targetPoint);
            float gravityTime = _speed / distance * Time.fixedDeltaTime;

            transform.position = Vector3.Lerp(transformPosition, targetPoint, gravityTime);
            _velocity = transformPosition - targetPoint;
        }

        private void Rotation() =>
            transform.Rotate(_rotation * _constructionData.RotationSpeed * Time.fixedDeltaTime);

        private void CollisionDetect() =>
            _elements.ForEach(x => x.CollisionDetect());

        private void OnCollision(ElementView[] elements) =>
            OnCollisionHandler?.Invoke(elements);
    }
}