using System;
using System.Collections.Generic;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public abstract class FigureBuilderBase : MonoBehaviour
    {
        [SerializeField] protected int _figureHeight;

        private List<BuildBlock> _unbrokenBlocks = new List<BuildBlock>();
        private IGameFactory _gameFactory;
        public event Action BeforeDestroy;

        [Inject]
        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void Start() =>
            GenerateFigure();

        private void OnDestroy() =>
            BeforeDestroy?.Invoke();

        protected abstract void GenerateFigure();

        protected void CreateBlock(Vector3 position)
        {
            BuildBlock block = _gameFactory.CreateRandBuildBlockWithRandColor();
            block.transform.position = position;
            block.transform.SetParent(transform);
            
            AddUnbrokenBlock(block);
        }

        private void AddUnbrokenBlock(BuildBlock block)
        {
            _unbrokenBlocks.Add(block);
            block.OnBeforeDestroyTimerStart += RemoveFromUnbrokenList;
        }

        private void RemoveFromUnbrokenList(BuildBlock block)
        {
            block.OnBeforeDestroyTimerStart -= RemoveFromUnbrokenList;
            _unbrokenBlocks.Remove(block);

            DestroyFigureIfNoUnbrokenBlocks();
        }

        private void DestroyFigureIfNoUnbrokenBlocks()
        {
            if(_unbrokenBlocks.Count == 0)
                Destroy(gameObject);
        }

        public abstract int GetHeight();
        public abstract int GetWidth();
    }
}