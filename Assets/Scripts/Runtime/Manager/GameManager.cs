using System;
using UnityEngine;

/// <summary>
/// 游戏场景管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform heroSpawnPos; //角色生成的位置
    [SerializeField] private CameraTarget cameraT; //相机目标组件

    public static Transform playerTans { get; private set; } //角色的transform组件

    private IDataService _jsonDataService;
    private PanelManager _panelManager;
    private HeroList _heroList; //角色资源信息

    private void Awake()
    {
        _jsonDataService = new JsonDataService();
        _panelManager = new PanelManager();
        _heroList = new HeroList();
        HeroBorn();
        GameSet();
        SceneInit();
        CameraSet();
    }

    private void OnDestroy()
    {
        playerTans = null;
        _panelManager.PopAll();
    }

    //场景初始化
    public void SceneInit()
    {
        EnvironmentManager.Instance.BackGroundMove();
        AudioPlayerManager.Instance.PlayAudio(GameRoot.Instance.AudioList.idleBgm);
    }

    //角色生成
    private void HeroBorn()
    {
        GameObject newHero = Instantiate(Resources.Load<GameObject>(_heroList.Lewis), heroSpawnPos.position,
            Quaternion.identity);
        playerTans = newHero.transform;

        //同步数据
    }

    private void CameraSet()
    {
        cameraT.SetTarget(playerTans);
        cameraT.Follow();
    }

    private void GameSet()
    {
        playerTans.GetComponent<Health>().Dead += () =>
        {
            _panelManager.Push(new DeadPanel());
            BattleManager.Instance.BattleEixt();
            DroppedItem.ClearAllItem();
        };
    }
    //////////////////////////////////////////////////
}