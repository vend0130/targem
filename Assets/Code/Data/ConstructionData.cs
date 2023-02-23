using Code.Extensions;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(ConstructionData),
        menuName = "Static Data/" + nameof(ConstructionData), order = 1)]
    public class ConstructionData : ScriptableObject
    {
        [field: SerializeField] public Vector3Int _size = Vector3Int.one;
        [field: SerializeField] public PrimitiveType PrimitiveType { get; private set; } = PrimitiveType.Cube;
        [field: SerializeField] public float DistanceBetweenElements { get; private set; } = 1;

        public Vector3Int Size => _size;

        private void OnValidate()
        {
            if (_size.Multiply() > 0)
                return;

            _size.Change();
        }
    }
}