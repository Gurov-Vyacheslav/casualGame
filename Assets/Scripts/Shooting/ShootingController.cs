using LearnGame.Animations;
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

        protected CharacterAnimatorController _characterAnimatorController;

        protected void Awake()
        {
            _characterAnimatorController = GetComponent<CharacterAnimatorController>();
        }
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

            if (HasTarget) _characterAnimatorController.TargetPosition = TargetPosition;
            _characterAnimatorController.HasTarget = HasTarget;
        }

        public void SetWeapon( Weapon weaponPrefub, Transform hand)
        {
            if (_weapon != null) Destroy(_weapon.gameObject);
            _weapon = Instantiate(weaponPrefub, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localPosition = Vector3.zero;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, LayerUtils.CharacterMask);

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

        public bool SetBaseWeapon()
        {
            return !LayerUtils.IsBaseWeapon(_weapon.gameObject);
        }
    }
}