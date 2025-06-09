using UnityEngine;

namespace LearnGame.Movement
{
    public class JoystickPlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSourse
    {
        private UnityEngine.Camera _camera;
        private Joystick _joystick;
        public Vector3 MovementDirection { get; private set; } = Vector3.zero;
        public bool BoostIncluded { get; private set; }

        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _joystick = FindObjectOfType<Joystick>();
        }

        protected void Update()
        {
            if (_joystick == null) return;

            var horizontal = _joystick.Horizontal;
            var vertical = _joystick.Vertical;
            
            BoostIncluded = new Vector2 (horizontal, vertical).magnitude > 0.6;

            var direction = new Vector3(horizontal, 0, vertical);
            direction = _camera.transform.rotation * direction;
            
            direction.y = 0;
            MovementDirection = direction.normalized;
        }
    }
}