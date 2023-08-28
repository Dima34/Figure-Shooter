using Infrastructure.Constants;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _flyDirection;
        private float _speed;
        private float _lifeTime;


        public void Initialize(float speed, Vector3 flyDirection, float lifeTime)
        {
            _speed = speed;
            _flyDirection = flyDirection;
            _lifeTime = lifeTime;
        }

        private void Start() =>
            SetSelfDestroyTimer();

        private void SetSelfDestroyTimer() =>
            Destroy(gameObject, _lifeTime);

        private void Update() =>
            transform.position += _flyDirection * _speed * Time.deltaTime;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag != Tags.UNTAGGED) 
                Destroy(gameObject);
        }
    }
}