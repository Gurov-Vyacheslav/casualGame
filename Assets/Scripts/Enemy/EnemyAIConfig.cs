using UnityEngine;

namespace LearnGame.Enemy
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = nameof(CharacterConfig))]
    public class EnemyAIConfig : ScriptableObject, IEnemyAIConfig
    {
        [field: SerializeField]
        public float ViewRadius { get; private set; } = 50f;
        [field: SerializeField]
        public float MinHpForEscapePercent { get; private set; } = 30f;
        [field: SerializeField]
        public float ProbabilityEscapePercent { get; private set; } = 70f;
        [field: SerializeField]
        public float SafeDistance { get; private set; } = 20f;

    }
}
