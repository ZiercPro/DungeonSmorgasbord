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

                BindData(_model.ProjectileCount, a =>
                {
                    _view.ProjectileCountText.SetText(a.ToString());
                    _view.OnProjectileChanged();
                }, null);

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