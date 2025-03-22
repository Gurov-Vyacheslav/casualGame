using UnityEngine;

namespace LearnGame.Timer
{
    public class UnityTimer: ITimer
    {
        public float DeltaTime => Time.deltaTime;
    }
}