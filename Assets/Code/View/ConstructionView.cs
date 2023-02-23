using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.View
{
    public class ConstructionView : MonoBehaviour
    {
        private List<ElementView> _elements;
        private Transform _target;
        private Vector3 _force = Vector3.zero;
        private float _speed;

        //note: for test
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _force = Random.insideUnitSphere.normalized * 10;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Init(List<ElementView> elements, Transform target, float speed)
        {
            _elements = elements;
            _target = target;
            _speed = speed;
        }

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
    }
}