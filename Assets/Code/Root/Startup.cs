using Code.Data;
using Code.Factory;
using UnityEngine;

namespace Code.Root
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private ConstructionData _constructionData;

        private void Awake()
        {
            var factory = new GameFactory(_gameData, _constructionData);

            factory.Create();
        }
    }
}