using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovementController: IMovable
    {
        bool BoostSpeedIncluded { get; set; }

        Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection);
    }
}
