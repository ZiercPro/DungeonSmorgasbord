using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using ZiercCode.Audio;
using ZiercCode.ObjectPool;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame.Weapon.Weapon_LuGerGun
{
    public class Weapon_LuGerGun : MonoBehaviour
    {
        [SerializeField] private GameObject shellCase; //弹壳
        [SerializeField] private Transform shellCasePoint; //抛壳点

        [SerializeField] private Vector2 shellGroundVRange; //抛壳水平速度范围
        [SerializeField] private Vector2 shellVerticalVRange; //抛壳垂直速度范围
        [SerializeField] private Vector2 shellRotateVRange; //抛壳旋转速度范围

        [SerializeField] private AudioClip[] shootSfx; //射击音效 随机播放

        [Header("Recoil")] [SerializeField] private float duration;

        [SerializeField] private float strength;

        private Tween _recoilTween; //后座动画

        private void Awake()
        {
            PoolManager.Instance.Register("shellCase", shellCase);
        }

        public void PlayShootSfx()
        {
            AudioPlayer.Instance.PlaySfx(shootSfx[Random.Range(0, shootSfx.Length)], .3f);
        }

        //生成弹壳
        public void SpawnShellCase()
        {
            GameObject shell = (GameObject)PoolManager.Instance.Get("shellCase");
            shell.SetActive(true);
            shell.transform.position = shellCasePoint.position;
            shell.transform.rotation = quaternion.identity;

            shell.GetComponent<FakeHeight.FakeHeight>()
                .Init(Random.insideUnitCircle * Random.Range(shellGroundVRange.x, shellGroundVRange.y),
                    Random.Range(shellVerticalVRange.x, shellVerticalVRange.y),
                    true, Random.Range(shellRotateVRange.x, shellRotateVRange.y));
        }

        //后座
        public void Recoil()
        {
            if (_recoilTween == null)
                _recoilTween = transform.DOLocalMoveX(-strength, duration)
                    .OnComplete(() => transform.DOLocalMoveX(0f, duration)).SetAutoKill(false);
            else
            {
                _recoilTween.Restart();
            }
        }
    }
}