using RMC.Core.Observables;
using RMC.Mini;
using RMC.Mini.Controller;
using System;
using UnityEngine.Events;

namespace ZiercCode._DungeonGame.UI.WeaponInfo
{
    public class WeaponInfoController : BaseController<WeaponInfoModel, WeaponInfoView, WeaponInfoService>
    {
        public WeaponInfoController(WeaponInfoModel model, WeaponInfoView view, WeaponInfoService service) : base(model,
            view, service)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                BindData(_model.CurrentMagazineCount, a =>
                {
                    _view.CurrentMagazineCountText.SetText(a.ToString());
                    _view.OnProjectileChanged();
                }, null);

                BindData(_model.FireDamage, a => _view.FireDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.IceDamage, a => _view.IceDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.WoodDamage, a => _view.WoodDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.ElectricDamage, a => _view.ElectricDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.WindDamage, a => _view.WindDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.VoiceDamage, a => _view.VoiceDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.PoisonDamage, a => _view.PoisonDamageText.SetText(a.ToString("F4")), null);
                BindData(_model.VoidDamage, a => _view.VoidDamageText.SetText(a.ToString("F4")), null);

                BindData(_model.HitForce, a => _view.HitForceText.SetText(a.ToString("F4")), null);
                BindData(_model.CriticalChance, a => _view.CriticalChanceText.SetText(a.ToString("F4")), null);
                BindData(_model.CriticalRate, a => _view.CriticalDamageRateText.SetText(a.ToString("F4")), null);
                BindData(_model.TriggerChance, a => _view.TriggerChanceText.SetText(a.ToString("F4")), null);
                BindData(_model.DamageReductionDistanceReduction,
                    a => _view.DamageReductionByDistanceText.SetText(a.ToString("F4")),
                    null);
                BindData(_model.ShootSpeed, a => _view.ShootSpeedText.SetText(a.ToString("F4")), null);
                BindData(_model.ShootDistance, a => _view.ShootDistanceText.SetText(a.ToString("F4")), null);
                BindData(_model.CurrentMagazineCount, a => _view.CurrentMagazineCountText.SetText(a.ToString()), null);
                BindData(_model.MagazineCapacity, a => _view.MagazineCapacityText.SetText(a.ToString()), null);
                BindData(_model.ReloadTime, a => _view.ReloadTimeText.SetText(a.ToString("F4")), null);
                BindData(_model.Accuracy, a => _view.AccuracyText.SetText(a.ToString("F4")), null);
                BindData(_model.ProjectileNumPerShoot, a => _view.ProjectileNumPerShootText.SetText(a.ToString("F4")),
                    null);
                BindData(_model.ProjectileSpeed, a => _view.ProjectileSpeedText.SetText(a.ToString("F4")), null);
                BindData(_model.ProjectileSize, a => _view.ProjectileSizeText.SetText(a.ToString("F4")), null);
                BindData(_model.WeaponName, a => _view.WeaponName.SetText(a), null);

                _service.OnReloaded.AddListener(OnWeaponReloaded);
                _service.OnStartReload.AddListener(OnWeaponStartReload);

                _service.SyncModel();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _service.OnReloaded.RemoveListener(OnWeaponReloaded);
            _service.OnStartReload.RemoveListener(OnWeaponStartReload);
        }

        //model和view数据绑定 也可以进行逻辑绑定
        private void BindData<T>(Observable<T> modelData, Action<T> callBack, UnityEvent<T> viewData)
        {
            //注册model变化回调
            modelData.OnValueChanged.AddListener((pre, cur) => callBack?.Invoke(cur));
            //注册view变化回调
            viewData?.AddListener(v =>
            {
                modelData.Value = v;
            });
        }

        private void OnWeaponStartReload(float duration)
        {
            _view.ReloadAnimation.PlayReloadAnimation(duration);
        }

        private void OnWeaponReloaded()
        {
            _service.SyncModel();
        }
    }
}