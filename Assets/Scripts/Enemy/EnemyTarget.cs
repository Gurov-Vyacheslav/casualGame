using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest {  get; private set; }

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly PlayerCharacter _player;

        private readonly Collider[] _coliders = new Collider[10];

        private readonly float _safeDistance = 20f;

        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
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
            float minDistance = float.MaxValue;
            var count = FindAllDistance(LayerUtils.PickUpMask | LayerUtils.СharacterMask);

            for (int i = 0; i < count; i++)
            {
                var go = _coliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;

                var distance = DistanceFromAgentTo(go);
                if (distance < minDistance ) 
                {
                    minDistance = distance;
                    Closest = go;
                }
            }
            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)
                Closest = _player.gameObject;
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
