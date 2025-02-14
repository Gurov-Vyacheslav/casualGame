using LearnGame.Movement;
using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest {  get; private set; }

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly CharacterMovementController _enemyMovementController;
        private readonly ShootingController _shootingController;
        private readonly PlayerCharacter _player;

        private readonly Collider[] _coliders = new Collider[10];

        private readonly float _safeDistance = 20f;

        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius, 
            CharacterMovementController enemyMovementController, ShootingController shootingController)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
            _enemyMovementController = enemyMovementController;
            _shootingController = shootingController;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                return DistanceFromAgentTo(Closest);
            return 0;
        }
        public bool MoveToSafeDistance()
        {
            if (Closest != null)
                return (_agentTransform.position - Closest.transform.position).magnitude >= _safeDistance;
            return true;
        }

        public void FindClosest()
        {
            if (_shootingController.SetBaseWeapon())
            {
                if (_enemyMovementController.BoostSpeedIncluded)
                    FindClosestByMasksHeirarchy(new int[] { LayerUtils.CharacterMask });
                else
                    FindClosestByMasksHeirarchy(new int[] { LayerUtils.CharacterMask | LayerUtils.PickUpBoosterMask});
            }
            else
            {
                if (_enemyMovementController.BoostSpeedIncluded)
                    FindClosestByMasksHeirarchy(new int[] { LayerUtils.PickUpWeaponMask, LayerUtils.CharacterMask});
                else
                    FindClosestByMasksHeirarchy(new int[] { LayerUtils.PickUpWeaponMask, LayerUtils.PickUpBoosterMask, LayerUtils.CharacterMask });
            }
            Debug.Log(Closest.gameObject.name);
        }
        private void FindClosestByMasksHeirarchy(int[] layerMasks)
        {
            foreach (int mask in layerMasks)
                if (FindClosestByMask(mask)) return;
            if (_player != null) Closest = _player.gameObject;
        }
        private bool FindClosestByMask(int layerMask)
        {
            bool findClosest = false;
            float minDistance = float.MaxValue;
            var count = FindAllDistance(layerMask);
            for (int i = 0; i < count; i++)
            {
                var go = _coliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;

                var distance = DistanceFromAgentTo(go);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    Closest = go;
                    findClosest = true;
                }
            }
            return findClosest;
        }
        private int FindAllDistance(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agentTransform.position,
                _viewRadius,
                _coliders,
                layerMask);
            return size;
        }
        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;
    }
}
