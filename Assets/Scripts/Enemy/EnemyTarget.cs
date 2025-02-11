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

        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                return DistanceFrimAgentTo(Closest);
            return 0;
        }

        public void FindClosest()
        {
            float minDistance = float.MaxValue;
            var count = FindAllDistance(LayerUtils.PickUpMask | LayerUtils.СharacterMask);

            for (int i = 0; i < count; i++)
            {
                var go = _coliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;

                var distance = DistanceFrimAgentTo(go);
                if (distance < minDistance ) 
                {
                    minDistance = distance;
                    Closest = go;
                }
            }
            if (_player != null && DistanceFrimAgentTo(_player.gameObject) < minDistance)
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
        private float DistanceFrimAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;
    }
}
