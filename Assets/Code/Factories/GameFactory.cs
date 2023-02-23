using System.Collections.Generic;
using Code.Datas;
using Code.Views;
using UnityEngine;

namespace Code.Factories
{
    public class GameFactory
    {
        public List<ConstructionView> Constructions { get; private set; }

        private const string PoolName = "Constructions";
        private const string ConstructionName = "Construction";

        private readonly GameData _gameData;
        private readonly ConstructionData _constructionData;
        private readonly Transform _target;

        private bool RandomChance => Random.value > .5f;

        public GameFactory(GameData gameData, ConstructionData constructionData, Transform target)
        {
            _gameData = gameData;
            _constructionData = constructionData;
            _target = target;
        }

        public void Create()
        {
            Transform poolParent = new GameObject(PoolName).transform;
            Constructions = new List<ConstructionView>(_gameData.NumberConstructionOnScene);

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

            List<ElementView> elements = CreateElements(construction);

            ConstructionView constructionView = construction.gameObject.AddComponent<ConstructionView>();
            constructionView.Init(elements, _target, _constructionData.Speed, _constructionData);

            Constructions.Add(constructionView);
        }

        private List<ElementView> CreateElements(Transform prent)
        {
            List<ElementView> elements = new List<ElementView>();

            for (int x = 0; x < _constructionData.Size.x; x++)
            {
                for (int y = 0; y < _constructionData.Size.y; y++)
                {
                    for (int z = 0; z < _constructionData.Size.z; z++)
                    {
                        if (TryCreateElement(prent, new Vector3Int(x, y, z), out ElementView element))
                            elements.Add(element);
                    }
                }
            }

            return elements;
        }

        private bool TryCreateElement(Transform parent, Vector3Int indexPosition, out ElementView elementView)
        {
            if (RandomChance)
            {
                elementView = null;
                return false;
            }

            var element = GameObject.CreatePrimitive(_constructionData.PrimitiveType);
            element.transform.SetParent(parent);

            Vector3 position = CalculateElementPosition(indexPosition);
            element.transform.localPosition = position;

            element.name = $"{_constructionData.PrimitiveType}_{position}";

            elementView = element.AddComponent<ElementView>();
            elementView.ChangeColor(_constructionData.DefaultColor);
            return true;
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