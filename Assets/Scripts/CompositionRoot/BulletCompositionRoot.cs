using LearnGame.Animations;
using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.CompositionRoot
{
    public class BulletCompositionRoot: CompositionRoot<BulletView>
    {
        private BulletDescription _bulletDescription;
        private Vector3 _direction;

        private BulletView _view;

        public void Initialize(Vector3 direction, BulletDescription bulletDescription)
        {
            _bulletDescription = bulletDescription;
            _direction = direction;
        }
        public override BulletView Compose(ITimer timer)
        {
            _view = GetComponent<BulletView>();

            IMemorizeMovable movementController = new BulletMovementController(_bulletDescription.BulletFlySpeed, timer);

            var bullet = new BulletModel(_direction, _bulletDescription, movementController);
            _view.Initialize(bullet);

            return _view;
        }

    }
}
