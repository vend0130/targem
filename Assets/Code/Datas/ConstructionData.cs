using Code.Extensions;
using UnityEngine;

namespace Code.Datas
{
    [CreateAssetMenu(fileName = nameof(ConstructionData),
        menuName = "Static Data/" + nameof(ConstructionData), order = 1)]
    public class ConstructionData : ScriptableObject
    {
        [field: SerializeField] public float MinHitForce { get; private set; } = 200;
        [field: SerializeField] public float MaxHitForce { get; private set; } = 250;
        [Space] [SerializeField] private Vector3Int _size = Vector3Int.one;
        [field: SerializeField] public PrimitiveType PrimitiveType { get; private set; } = PrimitiveType.Cube;
        [field: SerializeField] public float DistanceBetweenElements { get; private set; } = 1;
        [field: SerializeField] public Color DefaultColor { get; private set; } = Color.white;
        [field: SerializeField] public Color HitColor { get; private set; } = Color.red;

        public Vector3Int Size => _size;
        public float DistanceForActivateCollider => 5;
        public int RotationSpeed => 1;

        private void OnValidate()
        {
            if (_size.Multiply() > 0)
                return;

            _size.Change();
        }
    }
}