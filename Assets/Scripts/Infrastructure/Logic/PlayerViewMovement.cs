using System;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class PlayerViewMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _head;

        private IInputService _inputService;
        private PlayerStaticData _playerData;
        private IStaticDataService _staticDataService;


        [Inject]
        public void Construct(IInputService inputService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _inputService = inputService;
        }

        private void Start() =>
            _playerData = _staticDataService.GetPlayerData();

        private void Update()
        {
            TryApplyXHeadRotation();
            TryApplyYHeadRotation();
        }

        private void TryApplyXHeadRotation()
        {
            TryApplyHeadRotation(
                _inputService.Axis.x, 
                _playerData.HorizontalHeadRotationSpeed, 
                transform.up, 
                Space.World);
        }

        private void TryApplyYHeadRotation()
        {
            TryApplyHeadRotation(
                -_inputService.Axis.y, 
                _playerData.VerticalHeadRotationSpeed, 
                transform.right, 
                Space.Self);
        }

        private void TryApplyHeadRotation(float axis, float speed, Vector3 rotAxis, Space rotRelative)
        {
            if (axis != 0)
            {
                float rotAngle = axis * speed * Time.deltaTime;
                AddHeadRotation(rotAngle, rotAxis, rotRelative);
            }
        }

        private void AddHeadRotation(float rotAngle, Vector3 rotAxis, Space relativeTo) =>
            _head.transform.Rotate(rotAxis, rotAngle, relativeTo);
    }
}