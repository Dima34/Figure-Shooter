using System;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        private IGameFactory _gameFactory;
        private IInputService _inputService;
        private PlayerStaticData _playerData;
        private bool _canShot;
        private float _timeToShot;

        [Inject]
        public void Construct(IGameFactory gameFactory, IInputService inputService, IStaticDataService staticDataService)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _playerData = staticDataService.GetPlayerData();
        }

        private void Update()
        {
            if (_inputService.IsFire)
                CreateAndInitializeBullet();
        }

        private void CreateAndInitializeBullet()
        {
            Bullet bullet = _gameFactory.CreateBullet();
            bullet.transform.position = transform.position; 
            
            bullet.Initialize(_playerData.BulletSpeed, _mainCamera.transform.forward, _playerData.BulletLifeTime);
        }
    }
}