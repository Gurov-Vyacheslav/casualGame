using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMemorizeMovable:IMovable
    {
       public float CurrentDistance { get; }
    }
}
