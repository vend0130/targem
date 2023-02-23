using Code.Controllers;
using Code.Datas;
using Code.Factories;
using UnityEngine;

namespace Code.Root
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private ConstructionData _constructionData;
        [SerializeField] private Transform _targetPoint;

        private void Awake()
        {
            GameFactory factory = new GameFactory(_gameData, _constructionData, _targetPoint);
            ConstructionsController constructionsController = new ConstructionsController(factory, _constructionData);

            factory.Create();
            constructionsController.Init();
        }
    }
}