using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.NPC
{
    //结合navmesh 实现实体随机移动
    public class RandomMoveWithNavMesh : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float range; //随机移动的范围 以npc自己为中心
        [SerializeField] private float minWaitTime; //停止移动后的最小等待时间
        [SerializeField] private float maxWaitTime; //停止移动后的最大等待时间

        private Coroutine _randomMoveCoroutine;

        private void Awake()
        {
            _randomMoveCoroutine = StartCoroutine(RandomMove());
        }

        private Vector2 GetRandomPosition()
        {
            return MyMath.GetRandomPos(transform.position, new Vector2(range, range));
        }

        private IEnumerator RandomMove()
        {
            float stopTime;
            while (true)
            {
                if (navMeshAgent.velocity == Vector3.zero)
                {
                    stopTime = Random.Range(minWaitTime, maxWaitTime);
                    yield return new WaitForSeconds(stopTime);
                    Vector2 randomPosition = GetRandomPosition();
                    navMeshAgent.destination = randomPosition;
                }

                yield return null;
            }
        }

        public void StopRandomMove()
        {
            if (_randomMoveCoroutine != null)
            {
                StopCoroutine(_randomMoveCoroutine);
            }
        }

        public void StartRandomMove()
        {
            StopRandomMove();
            _randomMoveCoroutine = StartCoroutine(RandomMove());
        }
    }
}