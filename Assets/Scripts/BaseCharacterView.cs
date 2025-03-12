using LearnGame.Animations;
using LearnGame.Boosters;
using LearnGame.Movement;
using LearnGame.PickUp;
using LearnGame.Shooting;
using LearnGame.Spawners;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(CharacterController), typeof(CharacterAnimatorController), typeof(PowerUpController))]
    public abstract class BaseCharacterView : MonoBehaviour
    {
        [SerializeField]
        private WeaponFactory _baseWeaponFactory;

        [SerializeField]
        private Transform _hand;


        [Space(10)]
        [Header("Particle Settings")]
        [SerializeField] private ParticleSystem _bloodSpatter;
        [SerializeField] private ParticleSystem _dieParticle;

        [Header("Audio Settings")]
        [SerializeField] private AudioSource _dieSound;

        private bool _isDead = false;

        private IMovementDirectionSourse _movementDirectionSourse;

        private WeaponView _weapon;

        private PowerUpController _powerUpController;
        private CharacterController _characterController;
        protected CharacterAnimatorController _characterAnimatorController;

        public BaseCharacterModel Model { get; private set; }

        protected virtual void Awake()
        {
            _movementDirectionSourse = GetComponent<IMovementDirectionSourse>();
            _powerUpController = GetComponent<PowerUpController>();
            _characterAnimatorController = GetComponent<CharacterAnimatorController>();
            _characterController = GetComponent<CharacterController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponFactory, true);
        }

        public void Initialize(BaseCharacterModel model)
        {
            Model = model;
            Model.Initialize(transform.position, transform.rotation);
        }

        protected virtual void Update()
        {
            if (CheckVictory() || CheckDie()) return;

            var direction = _movementDirectionSourse.MovementDirection;
            var boostIncluded = _movementDirectionSourse.BoostIncluded;

            Model.Move(direction, boostIncluded);
            Model.TryShoot(_weapon.BulletSpawnPosition.position);

            var moveDelta = Model.Transform.Position - transform.position;
            _characterController.Move(moveDelta);
            Model.Transform.Position = transform.position;

            transform.rotation = Model.Transform.Rotation;

            if (Model.MovingForward)
                _characterAnimatorController.MovingForward();
            else
                _characterAnimatorController.MovingBackwards();

        }

        protected void OnTriggerEnter(Collider other)
        {
            if (_isDead) return;

            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                Model.Damage(bullet.Damage);

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

        public void SetWeapon(WeaponFactory weaponFactory, bool isBaseWeaapon = false)
        {
            if (_weapon != null)
                Destroy(_weapon);
            _weapon = weaponFactory.Create(_hand);

            Model.SetWeapon(_weapon.Model, isBaseWeaapon);
        }

        public void GetBoostSpeed(SpeedBooster speedBooster)
        {
            _powerUpController.GetSpeedBooster(speedBooster);
        }

        private bool CheckDie()
        {
            if (Model.IsDead && !_isDead)
            {
                SetSettingBeforeDie();
            }
            return Model.IsDead;
        }

        protected virtual void SetSettingBeforeDie()
        {
            _isDead = true;
            _characterAnimatorController.IsDead();
            _dieParticle.Play();
            _dieSound.Play();
        }

        protected abstract void OnDestroy();

        protected abstract bool CheckVictory();
    }
}

