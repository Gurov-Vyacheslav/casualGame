using UnityEngine;

namespace LearnGame.Shooting
{
    public interface IShootingTarget
    {
        BaseCharacterModel GetTarget(Vector3 position, float radius);
    }
}
