using LearnGame.Camera;
using LearnGame.Movement;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        protected override void Awake() 
        {
            base.Awake();
            var cameraController = UnityEngine.Camera.main.GetComponent<CameraController>();
            cameraController.SetPlayer(this);
        }
    }
}

