using System;
using System.Collections.Generic;
using Infrastructure.Constants;
using Infrastructure.Logic;
using Infrastructure.Services.Random;
using Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private DiContainer _container;
        private IStaticDataService _staticDataService;
        private IRandomService _randomService;
        private GameObject _player;
        private PlayerViewMovement _playerViewMovement;
        private SmoothlyLookAt _playerSmoothlyLookAt;
        
        public GameObject Player => _player;
        public PlayerViewMovement PlayerViewMovement => _playerViewMovement;
        public SmoothlyLookAt PlayerSmoothlyLookAt => _playerSmoothlyLookAt;
        
        public GameFactory(DiContainer container, IStaticDataService staticDataService, IRandomService randomService)
        {
            _randomService = randomService;
            _staticDataService = staticDataService;
            _container = container;
        }

        public GameObject CreatePlayerAndAssingPlayerModules()
        {
            _player = _container.InstantiatePrefabResource(ResourcePaths.PLAYER);
            _playerViewMovement = _player.GetComponent<PlayerViewMovement>();
            _playerSmoothlyLookAt = _player.GetComponentInChildren<SmoothlyLookAt>();

            return _player;
        }

        public GameObject CreateMap() =>
            _container.InstantiatePrefabResource(ResourcePaths.MAP);

        public Bullet CreateBullet()
        {
            Bullet bullet = _container.InstantiatePrefabResource(ResourcePaths.BULLET).GetComponent<Bullet>();
            SetRandomBulletColor(bullet);

            return bullet;
        }

        private void SetRandomBulletColor(Bullet bullet)
        {
            List<Color> bulletColors = _staticDataService.GetBulletColors();
            
            Renderer renderer = bullet.gameObject.GetComponent<Renderer>();
            renderer.material.color = _randomService.GetRandomListElement(bulletColors);
        }

        public BuildBlock CreateRandBuildBlockWithRandColor()
        {
            List<string> variantPaths = _staticDataService.GetBuildBlockVariantPaths();
            BuildBlock block = InstantiateRandomFromPathList<BuildBlock>(variantPaths);
            
            List<Color> blockColors = _staticDataService.GetBlockColors();
            Color randomBlockColor = _randomService.GetRandomListElement(blockColors); 
            
            block.Initialize(randomBlockColor);
            
            return block;
        }

        public FigureBuilderBase CreateRandomFigure()
        {
            List<string> variantPaths = _staticDataService.GetFigureVatiantPaths();
            return InstantiateRandomFromPathList<FigureBuilderBase>(variantPaths);
        }

        private T InstantiateRandomFromPathList<T>(List<string> pathList)
        {
            string randomVariantPath = _randomService.GetRandomListElement(pathList);
            return _container.InstantiatePrefabResource(randomVariantPath).GetComponent<T>();
        }
    }
}