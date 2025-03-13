using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class BulletView : MonoBehaviour
    {
        public BulletModel Model { get; private set; }


        public void Initialize(BulletModel bulletModel)
        {
            Model = bulletModel;
            Model.Initialize(transform.position, transform.rotation);
            Model.Dead += Dead;
        }
 
        protected void Update()
        {
            Model.Move();
            transform.position = Model.Transform.Position;
            Model.Fly();
        }

        private void Dead() => Destroy(this.gameObject);
        private void OnDestroy() => Model.Dead -= Dead;
    }
}