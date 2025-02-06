using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovementDirectionSourse
    {
        Vector3 MovementDirection { get; }
        public bool BoostIncluded { get; }
    }
}
