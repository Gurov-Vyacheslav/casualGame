using UnityEngine;

namespace LearnGame
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = nameof(CharacterConfig))]
    public class CharacterConfig: ScriptableObject, ICharacterConfig
    {
        [field: SerializeField]
        public float Health { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public float BoostSpeed { get; private set; }

        [field: SerializeField]
        [Tooltip("Aka rotation speed")]
        public float MaxRadiansDelta { get; private set; }
    }
}
