using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.CompositionRoot
{
    public abstract class CompositionRoot<T>: MonoBehaviour where T : MonoBehaviour
    {
        public abstract T Compose(ITimer timer);
    }
}
