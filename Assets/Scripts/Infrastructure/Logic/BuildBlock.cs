using System;
using Infrastructure.Constants;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class BuildBlock : MonoBehaviour
    {
        [SerializeField] private Renderer _unbroken;
        [SerializeField] private GameObject _shattered;
        [SerializeField] private float _shatteredLifeTime;

        public event Action<BuildBlock> OnBeforeDestroyTimerStart;
        
        public void Initialize(Color blockColor)
        {
            SetUnbrokenColor(blockColor);
            SetShatteredColor(blockColor);
        }

        private void SetUnbrokenColor(Color blockColor) =>
            SetColor(_unbroken, blockColor);

        private void SetShatteredColor(Color blockColor)
        {
            foreach (Renderer renderer in _shattered.GetComponentsInChildren<Renderer>())
                SetColor(renderer, blockColor);
        }

        private static void SetColor(Renderer renderer, Color blockColor) =>
            renderer.material.color = blockColor;

        private void OnCollisionEnter(Collision collision)
        {
            if (IsBullet(collision))
            {
                DisableWholeBlock();
                RemoveTag();
                EnableShatteredBlock();
                UnParent();
                StartDestroyTimer();
            }
        }

        private void RemoveTag() =>
            gameObject.tag = Tags.UNTAGGED;

        private void UnParent() =>
            transform.parent = null;

        private static bool IsBullet(Collision collision) =>
            collision.gameObject.tag == Tags.BULLET;

        private void DisableWholeBlock() =>
            _unbroken.gameObject.SetActive(false);

        private void EnableShatteredBlock() =>
            _shattered.gameObject.SetActive(true);

        private void StartDestroyTimer()
        {
            OnBeforeDestroyTimerStart?.Invoke(this);
            Destroy(gameObject, _shatteredLifeTime);
        }
    }
}