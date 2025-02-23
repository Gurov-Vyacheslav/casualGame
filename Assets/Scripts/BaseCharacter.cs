using LearnGame.Boosters;
using LearnGame.Movement;
using LearnGame.PickUp;
using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [field: SerializeField]
        public float Health { get; private set; } = 2f;

        private IMovementDirectionSourse _movementDirectionSourse;

        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
        private PowerUpController _powerUpController;

        protected virtual void Awake()
        {
            _movementDirectionSourse = GetComponent<IMovementDirectionSourse>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
            _powerUpController = GetComponent<PowerUpController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        protected virtual void Update()
        {
            var direction = _movementDirectionSourse.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
            {
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            }
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            var boostIncluded = _movementDirectionSourse.BoostIncluded;
            _characterMovementController.BoostSpeedIncluded = boostIncluded;
            if (Health <= 0) Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                Health -= bullet.Damage;
                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                pickUp.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }

        public void GetBoostSpeed(SpeedBooster speedBooster)
        {
            _powerUpController.GetSpeedBooster(speedBooster);
        }
    }
}

