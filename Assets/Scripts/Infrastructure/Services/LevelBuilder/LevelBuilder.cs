using System;
using Infrastructure.Factories;
using Infrastructure.Logic;
using Infrastructure.Services.Random;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.LevelBuilder
{
    public class LevelBuilder : ILevelBuilder
    {
        private IGameFactory _gameFactory;
        private LevelStaticData _levelData;
        private FigureBuilderBase _figure;
        private IRandomService _randomService;
        
        private const int MAX_RAY_DISTANCE = 100;
        private const string WALL_MASK_NAME = "Wall";

        public LevelBuilder(IGameFactory gameFactory, IStaticDataService staticDataService, IRandomService randomService)
        {
            _gameFactory = gameFactory;
            _randomService = randomService;
            _levelData = staticDataService.GetCurrentLevelData();
        }
        
        public void BuildLevel()
        {
            CreatePlayer();
            CreateMap();
            CreateFigure();
        }

        private void CreateFigure()
        {
            TryUnsubscribeFromPreviousFigureDestroy();
            
            _figure = _gameFactory.CreateRandomFigure();
            
            Vector3 spawnPosition = GetFigureSpawnPosition(_figure, _gameFactory.Player.transform);
            _figure.transform.position = spawnPosition;

            SubscribeToFigureDestroy();
            RotatePlayerToFigure();
        }

        private Vector3 GetFigureSpawnPosition(FigureBuilderBase figure, Transform playerTransform)
        {
            int figureHalfHeight = figure.GetHeight() / 2;
            int figureHalfWidth = figure.GetWidth() / 2;

            Vector3 randomSpawnDirection = UnityEngine.Random.insideUnitSphere;
            randomSpawnDirection.y = playerTransform.position.z;

            Ray ray = new Ray(playerTransform.position, randomSpawnDirection);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, MAX_RAY_DISTANCE,
                    LayerMask.GetMask(WALL_MASK_NAME)))
            {
                Vector3 spawnSafeZone = randomSpawnDirection * figureHalfWidth;
                Vector3 figureSpawnStart = playerTransform.position + spawnSafeZone;
                Vector3 figureSpawnEnd = hitInfo.point - spawnSafeZone;

                Vector3 figureSpawnPosition = _randomService.GetRandomVectorBetween(figureSpawnStart, figureSpawnEnd);
                figureSpawnPosition.y = playerTransform.position.y + figureHalfHeight;
                
                return figureSpawnPosition;
            }

            return new Vector3();
        }

        private void TryUnsubscribeFromPreviousFigureDestroy()
        {
            if (_figure != null)
                _figure.BeforeDestroy -= CreateFigure;
        }

        private void SubscribeToFigureDestroy() =>
            _figure.BeforeDestroy += CreateFigure;

        private void RotatePlayerToFigure() =>
            _gameFactory.PlayerSmoothlyLookAt.LookAt(_figure.transform);

        private void CreatePlayer()
        {
            GameObject player = _gameFactory.CreatePlayerAndAssingPlayerModules();
            player.transform.position = _levelData.PlayerStartPosition.AsUnityVector();
        }

        private void CreateMap()
        {
            GameObject world = _gameFactory.CreateMap();
            world.transform.position = _levelData.WorldSpawnPosition.AsUnityVector();
        }

        
    }
}