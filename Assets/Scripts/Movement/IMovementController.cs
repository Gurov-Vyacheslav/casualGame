using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovementController
    {
        bool BoostSpeedIncluded { get; set; }
        Vector3 Translate(Vector3 movementDirection);

        Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection);
    }
}
