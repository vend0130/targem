using Code.Data;
using UnityEngine;

namespace Code.Factory
{
    public class GameFactory
    {
        private const string PoolName = "Constructions";
        private const string ConstructionName = "Construction";

        private readonly GameData _gameData;
        private readonly ConstructionData _constructionData;

        private bool RandomChance => Random.value > .5f;

        public GameFactory(GameData gameData, ConstructionData constructionData)
        {
            _gameData = gameData;
            _constructionData = constructionData;
        }

        public void Create()
        {
            Transform poolParent = new GameObject(PoolName).transform;

            for (int i = 0; i < _gameData.NumberConstructionOnScene; i++)
            {
                CreateConstruction(poolParent);
            }
        }

        private void CreateConstruction(Transform poolParent)
        {
            Transform construction = new GameObject(ConstructionName).transform;
            construction.SetParent(poolParent);
            construction.localPosition = Random.insideUnitSphere * _gameData.RadiusForSpawnConstructions;

            for (int x = 0; x < _constructionData.Size.x; x++)
            {
                for (int y = 0; y < _constructionData.Size.y; y++)
                {
                    for (int z = 0; z < _constructionData.Size.z; z++)
                    {
                        CreateElement(construction, new Vector3Int(x, y, z));
                    }
                }
            }
        }

        private void CreateElement(Transform parent, Vector3Int indexPosition)
        {
            if (RandomChance)
                return;

            var element = GameObject.CreatePrimitive(_constructionData.PrimitiveType);
            element.transform.SetParent(parent);

            Vector3 position = CalculateElementPosition(indexPosition);
            element.transform.localPosition = position;

            element.name = $"{_constructionData.PrimitiveType}_{position}";
        }

        private Vector3 CalculateElementPosition(Vector3Int indexPosition) =>
            new Vector3(
                Calculate(_constructionData.Size.x, indexPosition.x),
                Calculate(_constructionData.Size.y, indexPosition.y),
                Calculate(_constructionData.Size.z, indexPosition.z)
            );

        private float Calculate(int lenght, int index)
        {
            float distance = _constructionData.DistanceBetweenElements;

            float firstPoint = -((float)(lenght - 1) / 2) * distance;

            return firstPoint + index * distance;
        }
    }
}