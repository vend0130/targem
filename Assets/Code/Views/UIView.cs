using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public class UIView : MonoBehaviour
    {
        [field: SerializeField] public Button ResetButton { get; private set; }

        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private string _timerTextPrefix = "Время прошло:";

        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private string _counterTextPrefix = "Столкновений:";

        private float _seconds;

        private void Update()
        {
            _seconds += Time.deltaTime;
            _timerText.text = $"{_timerTextPrefix} {(int)_seconds}";
        }

        public void ResetTimer() =>
            _seconds = 0;

        public void ChangeTextCounter(int count) =>
            _counterText.text = $"{_counterTextPrefix} {count}";
    }
}