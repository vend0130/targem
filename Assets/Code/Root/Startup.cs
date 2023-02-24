using Code.Controllers;
using Code.Datas;
using Code.Factories;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Root
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private ConstructionData _constructionData;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private UIView _uiView;

        private void Awake()
        {
            GameFactory factory = new GameFactory(_gameData, _constructionData, _targetPoint);
            ConstructionsModel constructionsModel = new ConstructionsModel();
            UIController uiController = new UIController(_uiView, constructionsModel);
            ConstructionsController constructionsController =
                new ConstructionsController(factory, _constructionData, constructionsModel);

            factory.Create();
            uiController.Init();
            constructionsController.Init();
        }
    }
}