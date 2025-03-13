using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovable
    {
        Vector3 Translate(Vector3 movementDirection);
    }
}
