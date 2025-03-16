using TMPro;
using UnityEngine;
using ZiercCode._DungeonGame.HallScene;

namespace ZiercCode._DungeonGame.NPC
{
    //在玩家接近一定范围时显示名称，离开则消失
    public class ShowName : MonoBehaviour
    {
        private const int MAX_DETECT_NUM = 1; //最多扫描多少个碰撞体
        private bool _playerIsInRange; //范围内是否有player
        private RangeDetect _rangeDetect = new(MAX_DETECT_NUM);

        [SerializeField] private TextMeshPro textMeshPro; //显示名称的textMesh组件
        [SerializeField] private float minRadius = 0.8f; //最小显示范围，小于这个距离就显示名称


        //检测是否在范围内
        private bool IsPlayerInRange()
        {
            string playerTagString = "Player";
            return _rangeDetect.DetectByTagInCircle(playerTagString, transform.position, minRadius);
        }

        private void FixedUpdate()
        {
            _playerIsInRange = IsPlayerInRange();

            if (_playerIsInRange && !textMeshPro.isActiveAndEnabled)
            {
                textMeshPro.enabled = true;
            }
            else if (!_playerIsInRange && textMeshPro.isActiveAndEnabled)
            {
                textMeshPro.enabled = false;
            }
        }

        //Debug 绘制范围
        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(transform.position, minRadius);
        // }
    }
}