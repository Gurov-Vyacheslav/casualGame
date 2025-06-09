using UnityEngine;

namespace LearnGame.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSourse
    {
        private UnityEngine.Camera _camera;
        public Vector3 MovementDirection { get; private set; }
        public bool BoostIncluded => Input.GetKey(KeyCode.Space);

        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        protected void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var direction = new Vector3(horizontal, 0, vertical);
            direction = _camera.transform.rotation * direction;
            
            direction.y = 0;
            MovementDirection = direction.normalized;
        }
    }
}