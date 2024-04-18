using UnityEngine;
using ZiercCode.Core.UI;
using ZiercCode.Old.Component;
using ZiercCode.Old.Environment;
using ZiercCode.Old.Hero;
using ZiercCode.Old.UI.Panel;

namespace ZiercCode.Old.Manager
{
    /// <summary>
    /// 游戏场景管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        ///角色生成的位置
        /// </summary>
        [SerializeField] private Transform heroSpawnPos;
        /// <summary>
        /// 相机目标组件
        /// </summary>
        [SerializeField] private CameraTarget cameraT;

        /// <summary>
        /// 面板管理器
        /// </summary>
        private PanelManager _panelManager;

        /// <summary>
        /// 角色资源信息
        /// </summary>
        private HeroList _heroList;

        /// <summary>
        /// 英雄transform组件，用于全局引用
        /// </summary>
        public static Transform playerTrans { get; private set; } //角色的transform组件
        private void Awake()
        {
            _panelManager = new PanelManager();
            _heroList = new HeroList();
            HeroBorn();
            GameSet();
            SceneInit();
            CameraSet();
        }

        private void OnDestroy()
        {
            playerTrans = null;
            _panelManager.PopAll();
        }

        //场景初始化
        public void SceneInit()
        {
            ParallaxMoveManager.Instance.BackGroundMove();
        }

        //角色生成
        private void HeroBorn()
        {
            GameObject newHero = Instantiate(Resources.Load<GameObject>(_heroList.Lewis), heroSpawnPos.position,
                Quaternion.identity);
            playerTrans = newHero.transform;

            //同步数据
        }

        private void CameraSet()
        {
            cameraT.SetTarget(playerTrans);
            cameraT.Follow();
        }

        private void GameSet()
        {
            playerTrans.GetComponent<Health>().Dead += () =>
            {
                _panelManager.Push(new DeadPanel());
                DroppedItem.DroppedItem.ClearAllItem();
            };
        }
        //////////////////////////////////////////////////
    }
}