using LearnGame.Animations;
using LearnGame.Boosters;
using LearnGame.Movement;
using LearnGame.PickUp;
using LearnGame.Shooting;
using LearnGame.Spawners;
using UnityEngine;
using UnityEngine.Rendering;

namespace LearnGame
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController), typeof(CharacterAnimatorController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [field: SerializeField]
        public float Health { get; private set; } = 2f;


        [Space(10)]
        [Header("Particle Settings")]
        [SerializeField] private ParticleSystem _bloodSpatter;
        [SerializeField] private ParticleSystem _dieParticle;

        [Header("Particle Settings")]
        [SerializeField] private AudioSource _dieSound;

        public float MaxHealth { get; private set; }

        private bool _isDead = false;

        private IMovementDirectionSourse _movementDirectionSourse;

        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
        private PowerUpController _powerUpController;

        protected CharacterAnimatorController _characterAnimatorController;

        protected CharacterSpawnersController _characterSpawnerController;

        protected virtual void Awake()
        {
            _movementDirectionSourse = GetComponent<IMovementDirectionSourse>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
            _powerUpController = GetComponent<PowerUpController>();
            _characterAnimatorController = GetComponent<CharacterAnimatorController>();

            _characterSpawnerController = transform.parent.GetComponent<CharacterSpawner>().CharacterSpawnersController;

            MaxHealth = Health;
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

            if (CheckVictory() || CheckDie())
                direction = Vector3.zero;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            var boostIncluded = _movementDirectionSourse.BoostIncluded;
            _characterMovementController.BoostSpeedIncluded = boostIncluded;

            _characterAnimatorController.SetMoving(direction != Vector3.zero);
            _characterAnimatorController.SetRunning(boostIncluded);
            _characterAnimatorController.SetShooting(_shootingController.HasTarget);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                Health -= bullet.Damage;
                _bloodSpatter.Play();

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

        private bool CheckDie()
        {
            if (Health <= 0 && !_isDead)
            {
                SetSettingBeforeDie();
            }
            return Health <= 0;
        }

        protected virtual void SetSettingBeforeDie()
        {
            _isDead = true;
            _characterAnimatorController.IsDead();
            _dieParticle.Play();
            _dieSound.Play();

            _characterMovementController.enabled = false;
            _shootingController.enabled = false;
        }

        protected abstract void OnDestroy();

        protected abstract bool CheckVictory();
    }
}

