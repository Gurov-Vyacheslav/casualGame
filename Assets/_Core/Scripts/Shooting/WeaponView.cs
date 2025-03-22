using LearnGame.CompositionRoot;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform BulletSpawnPosition {get; private set;}

        [SerializeField]
        private BulletCompositionRoot _bulletPrefab;


        [SerializeField]
        private ParticleSystem _shootParticle;

        private AudioSource _shootSound;

        public WeaponModel Model { get; private set; }


        public void Initialize(WeaponModel model)
        {
            if (Model != null)
            {
                Debug.LogWarning("Weapon model has been already initialized!");
                return;
            }
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            Model = model;
            Model.Shot += Shoot;
        }

        protected void Awake()
        {
            _shootSound = GetComponent<AudioSource>();
        }
        public void Shoot(Vector3 targetDIrection, WeaponDescription description)
        {
            var bullet = Instantiate(_bulletPrefab, BulletSpawnPosition.position, Quaternion.identity);
            bullet.Initialize(targetDIrection, description.BulletDescription);
            bullet.Compose(GameManager.Instance.Timer);
            _shootParticle.Play();
            _shootSound.Play();
        }

        protected void OnDestroy()
        {
            if (Model != null)
                Model.Shot -= Shoot;
        }
    }
}