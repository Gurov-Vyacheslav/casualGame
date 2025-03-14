using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.CompositionRoot
{
    [RequireComponent(typeof(EnemyAICompositionRoot))]
    public class EnemyCompositionRoot: CharacterCompositionRoot
    {
        public override BaseCharacterView Compose(ITimer timer)
        {
            var enemyAICompositionRoot = GetComponent<EnemyAICompositionRoot>();
            enemyAICompositionRoot.Compose(timer);

             return base.Compose(timer);
        }
    }
}
