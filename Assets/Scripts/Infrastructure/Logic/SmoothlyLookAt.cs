using System.Collections;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class SmoothlyLookAt : MonoBehaviour
    {
        [SerializeField] private PlayerViewMovement _playerViewMovement;

        private Transform _target;
        private Coroutine _rotationCoroutine;
        private float _rotationSpeed;

        private const float STOP_TRESHOLD = 0.5f;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            PlayerStaticData playerData = staticDataService.GetPlayerData();
            _rotationSpeed = playerData.HorizontalHeadRotationSpeed;
        }

        public void LookAt(Transform at)
        {
            _target = at;

            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);

            _rotationCoroutine = StartCoroutine(RotateTowardsTarget());
        }

        private IEnumerator RotateTowardsTarget()
        {
            _playerViewMovement.enabled = false;

            while (true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                
                float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
                if (angleDifference < STOP_TRESHOLD)
                {
                    _playerViewMovement.enabled = true;
                    _rotationCoroutine = null;
                    yield break;
                }
                
                yield return null;
            }

            
        }
    }
}