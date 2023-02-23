﻿using System;
using UnityEngine;

namespace Code.Views
{
    public class ElementView : MonoBehaviour
    {
        public ConstructionView Construction { get; private set; }
        public Collider Collider { get; private set; }
        public event Action<ElementView[]> CollisionHandler;

        private Material _material;
        private Collider[] _hits = new Collider[7];

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            Collider = GetComponent<Collider>();
        }

        public void Init(ConstructionView construction)
        {
            Construction = construction;
        }

        public void CollisionDetect()
        {
            for (int i = 0; i < Hit(); i++)
            {
                if (_hits[i].TryGetComponent(out ElementView element) && element.Construction != Construction)
                {
                    CollisionHandler?.Invoke(new[] { this, element });
                }
            }
        }

        public void ChangeColor(Color color)
        {
            _material.color = color;
        }

        private int Hit() =>
            Physics.OverlapBoxNonAlloc(transform.position, Vector3.one / 2, _hits);
    }
}