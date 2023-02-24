using System;
using Code.Datas;
using Code.Factories;
using Code.Models;
using Code.Views;

namespace Code.Controllers
{
    public class ConstructionsController : IDisposable
    {
        private readonly GameFactory _factory;
        private readonly ConstructionData _constructionData;
        private readonly ConstructionsModel _constructionsModel;

        public ConstructionsController(GameFactory factory, ConstructionData constructionData,
            ConstructionsModel constructionsModel)
        {
            _factory = factory;
            _constructionData = constructionData;
            _constructionsModel = constructionsModel;
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

            elements[0].Construction.AddForce(elements[1].transform.position);
            elements[1].Construction.AddForce(elements[0].transform.position);

            _constructionsModel.HitCounter++;
        }
    }
}