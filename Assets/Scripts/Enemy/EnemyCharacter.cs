using Unity.VisualScripting;
using UnityEngine;

namespace LearnGame.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacter : BaseCharacter
    {
        public float MaxHealth { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            MaxHealth = Health;
        }
        protected void LateUpdate()
        {
            /*Debug.Log($"{Health}, {MaxHealth} {Health/MaxHealth * 100}");*/
        }
    }
}

