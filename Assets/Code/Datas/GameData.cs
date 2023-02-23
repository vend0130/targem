using UnityEngine;

namespace Code.Datas
{
    [CreateAssetMenu(fileName = nameof(GameData),
        menuName = "Static Data/" + nameof(GameData), order = 0)]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public int NumberConstructionOnScene { get; private set; } = 2;
        [field: SerializeField] public float RadiusForSpawnConstructions { get; private set; } = 5;
    }
}