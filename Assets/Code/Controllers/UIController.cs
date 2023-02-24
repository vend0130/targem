using System;
using Code.Models;
using Code.Views;

namespace Code.Controllers
{
    public class UIController : IDisposable
    {
        private readonly UIView _uiView;
        private readonly ConstructionsModel _constructionsModel;

        public UIController(UIView uiView, ConstructionsModel constructionsModel)
        {
            _uiView = uiView;
            _constructionsModel = constructionsModel;
        }

        public void Init()
        {
            _uiView.ResetButton.onClick.AddListener(ClickOnResetButton);
            _constructionsModel.HitCounterHandler += CounterChanged;
        }

        public void Dispose()
        {
            _uiView.ResetButton.onClick.RemoveListener(ClickOnResetButton);
            _constructionsModel.HitCounterHandler -= CounterChanged;
        }

        private void ClickOnResetButton()
        {
            _uiView.ResetTimer();
            _constructionsModel.HitCounter = 0;
        }

        private void CounterChanged() =>
            _uiView.ChangeTextCounter(_constructionsModel.HitCounter);
    }
}