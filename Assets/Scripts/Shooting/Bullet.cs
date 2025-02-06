using System.Collections;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public float Damage {  get; private set; }

        private Vector3 _direction;
        private float _flySpped;
        private float _maxFlyDistance;
        private float _currentFlyDistance;
        private float _damage;


        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed, float damage)
        {
            _direction = direction;
            _flySpped = flySpeed;
            _maxFlyDistance = maxFlyDistance;
            Damage = damage;
            
        }

 
        void Update()
        {
            var delta = _flySpped*Time.deltaTime;
            _currentFlyDistance += delta;
            transform.Translate(_direction * delta);
            if (_currentFlyDistance >= _maxFlyDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}