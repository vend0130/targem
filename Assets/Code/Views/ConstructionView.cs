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

        private const float Mass = 8;

        private List<ElementView> _elements;
        private Transform _target;
        private ConstructionData _constructionData;
        private Vector3 _moveForce;
        private Vector3 _velocity;
        private bool _collidersIsActive;
        private Vector3 _rotation;

        public void Constructor(List<ElementView> elements, Transform target, ConstructionData constructionData)
        {
            _elements = elements;
            _target = target;
            _constructionData = constructionData;
        }

        public void Init()
        {
            _collidersIsActive = true;

            for (var i = 0; i < _elements.Count; i++)
            {
                var element = _elements[i];
                element.Init(this);
                element.CollisionHandler += OnCollision;
            }

            _rotation = Random.rotation.eulerAngles;
        }

        private void FixedUpdate()
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);
            if (_moveForce.magnitude < _constructionData.MaxHitForce / 2 &&
                !_collidersIsActive && distanceToTarget < _constructionData.DistanceForActivateCollider)
                ChangeColliderState(true);

            Rotation(Time.fixedDeltaTime);
            Move(Time.fixedDeltaTime);
            CollisionDetect();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _elements.Count; i++)
                _elements[i].CollisionHandler -= OnCollision;
        }

        public void AddForce(Vector3 second)
        {
            Vector3 inNormal = second - transform.position;
            Vector3 velocity = _velocity == Vector3.zero ? -inNormal : _velocity;
            Vector3 reflect = -Vector3.Reflect(velocity, inNormal).normalized;
            _moveForce = reflect * Random.Range(_constructionData.MinHitForce, _constructionData.MaxHitForce);
            _rotation = _moveForce + Random.rotation.eulerAngles;
        }

        public void ChangeColliderState(bool value)
        {
            for (int i = 0; i < _elements.Count; i++)
                _elements[i].Collider.enabled = value;

            _collidersIsActive = value;
        }

        private void Move(float deltaTime)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = _target.position;

            float distance = Vector3.Distance(currentPosition, targetPosition);
            float timeGravity = deltaTime * (Mass / distance) * 2;
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, timeGravity);

            float timeForce = deltaTime / 2;
            newPosition = Vector3.Lerp(newPosition, targetPosition + _moveForce, timeForce);
            _moveForce = Vector3.Lerp(_moveForce, Vector3.zero, deltaTime * Mass / 2f);

            transform.position = newPosition;
        }

        private void Rotation(float deltaTime) =>
            transform.Rotate(_rotation * _constructionData.RotationSpeed * deltaTime);

        private void CollisionDetect()
        {
            if (!_collidersIsActive)
                return;

            for (int i = 0; i < _elements.Count; i++)
                _elements[i].CollisionDetect();
        }

        private void OnCollision(ElementView[] elements) =>
            OnCollisionHandler?.Invoke(elements);
    }
}