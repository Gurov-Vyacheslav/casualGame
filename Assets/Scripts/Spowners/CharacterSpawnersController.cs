using UnityEngine;
namespace LearnGame.Spowners
{
    public class CharacterSpawnersController : MonoBehaviour
    {
        [field: SerializeField]
        public int MinCountEnemy { get; private set; } = 2;
        [field: SerializeField]
        public int MaxCountEnemy { get; private set; } = 4;

        public bool PlayerWasSpowned { get; private set; } = false;

        public int CurrentCountEnemy { get; private set; } = 0;


        public void ReportSpownPlayer() => PlayerWasSpowned = true;
        public void ReportSpownEnemy() => CurrentCountEnemy++;
    }
}