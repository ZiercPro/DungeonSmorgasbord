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

    private IDataService jsonDataService;
    private Heros heros; //角色资源信息

    private void Awake()
    {
        jsonDataService = new JsonDataService();
        heros = new Heros();
        HeroBorn();
        SceneInit();
        CameraSet();
    }

    private void OnDestroy()
    {
        playerTans = null;
    }

    //场景初始化
    public void SceneInit()
    {
        EnvironmentManager.Instance.BackGroundMove();
    }

    //角色生成
    private void HeroBorn()
    {
        GameObject newHero = Instantiate(Resources.Load<GameObject>(heros.Lewis), heroSpawnPos.position,
            Quaternion.identity);
        playerTans = newHero.transform;

        //同步数据
    }

    private void CameraSet()
    {
        cameraT.SetTarget(playerTans);
        cameraT.Follow();
    }
    
    //////////////////////////////////////////////////
}