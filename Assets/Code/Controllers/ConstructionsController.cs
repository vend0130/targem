using System;
using Code.Datas;
using Code.Factories;
using Code.Views;

namespace Code.Controllers
{
    public class ConstructionsController : IDisposable
    {
        private readonly GameFactory _factory;
        private readonly ConstructionData _constructionData;

        public ConstructionsController(GameFactory factory, ConstructionData constructionData)
        {
            _factory = factory;
            _constructionData = constructionData;
        }

        public void Init() =>
            _factory.Constructions.ForEach(x => x.OnCollisionHandler += OnOnCollisionHandler);

        public void Dispose() =>
            _factory.Constructions.ForEach(x => x.OnCollisionHandler -= OnOnCollisionHandler);

        private void OnOnCollisionHandler(ElementView[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].Construction.ChangeColliderState(false);
                elements[i].ChangeColor(_constructionData.HitColor);
            }
        }
    }
}