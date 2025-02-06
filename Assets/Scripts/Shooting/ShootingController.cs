using System.Collections;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;

        private Weapon _weapon;

        private Collider[] _colliders = new Collider[2];
        private float _nextShootTimerSec;

        private GameObject _target;



        protected void Update()
        {
            _target = GetTarget();

            _nextShootTimerSec -= Time.deltaTime;
            if ( _nextShootTimerSec < 0 )
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);

                _nextShootTimerSec = _weapon.ShootFrequencySec;
            }
            

        }
        public void SetWeapon( Weapon weaponPrefub, Transform hand)
        {
            _weapon = Instantiate(weaponPrefub, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localPosition = Vector3.zero;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.EnemyMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);

            if (size > 0 )
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }
            return target;
        }
    }
}