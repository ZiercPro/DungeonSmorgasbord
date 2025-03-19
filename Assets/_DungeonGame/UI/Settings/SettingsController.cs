using RMC.Core.Observables;
using RMC.Mini;
using RMC.Mini.Controller;
using System;
using UnityEngine.Events;

namespace ZiercCode._DungeonGame.UI.Settings
{
    public class SettingsController : BaseController<SettingsModel, SettingsView, SettingsService>
    {
        public SettingsController(SettingsModel settingsModel,
            SettingsView settingsView,
            SettingsService settingsService) : base(settingsModel, settingsView, settingsService)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.BackButton.onClick.AddListener(OnBackButtonPressed);
                _view.ApplyButton.onClick.AddListener(OnApplyButtonPressed);

                //数据视图绑定//
                BindData(_model.MasterVolume, _view.MasterVolume.SetValueWithoutNotify,
                    _view.MasterVolume.onValueChanged);
                BindData(_model.EnvironmentVolume, _view.EnvironmentVolume.SetValueWithoutNotify,
                    _view.EnvironmentVolume.onValueChanged);
                BindData(_model.MusicVolume, _view.MusicVolume.SetValueWithoutNotify,
                    _view.MusicVolume.onValueChanged);
                BindData(_model.SfxVolume, _view.SfxVolume.SetValueWithoutNotify, _view.SfxVolume.onValueChanged);

                BindData(_model.FpsOn, _view.Fps.SetIsOnWithoutNotify, _view.Fps.onValueChanged);

                BindData(_model.Language, _view.LanguageDropdown.SetValueWithoutNotify,
                    _view.LanguageDropdown.onValueChanged);


                //数据逻辑绑定//
                BindData(_model.MasterVolume, a => _service.UpdateAudioVolume(_model), null);
                BindData(_model.MusicVolume, a => _service.UpdateAudioVolume(_model), null);
                BindData(_model.EnvironmentVolume, a => _service.UpdateAudioVolume(_model), null);
                BindData(_model.SfxVolume, a => _service.UpdateAudioVolume(_model), null);
                BindData(_model.Language, _service.UpdateLanguage, null);

                //todo fps显示

                Context.CommandManager.AddCommandListener<OpenSettingsCommand>(OnOpenSettingsCommand);

                _service.OnBackInput.AddListener(OnBackButtonPressed);

                _service.LoadGameSettings();
            }
        }

        //model和view数据绑定 也可以进行逻辑绑定
        private void BindData<T>(Observable<T> modelData, Action<T> callBack, UnityEvent<T> viewData)
        {
            //model变化改变view 但是不会广播值改变事件
            modelData.OnValueChanged.AddListener((pre, cur) => callBack?.Invoke(cur));
            //view变化改变model
            viewData?.AddListener(v =>
            {
                modelData.Value = v;
                _model.SettingChanged = true;
                _view.ApplyButton.gameObject.SetActive(true);
            });
        }

        private void OnOpenSettingsCommand(OpenSettingsCommand openSettingsCommand)
        {
            _service.SetUIInput(true);

            _view.gameObject.SetActive(true);

            _view.SettingsViewGroupUser.Enable();

            //重置设置修改状态
            _model.SettingChanged = false;
            _view.ApplyButton.gameObject.SetActive(false);

            _view.InitSubView();
        }

        private void OnBackButtonPressed()
        {
            _service.SetUIInput(false);
            _view.SettingsViewGroupUser.Disable();

            if (!_model.SettingChanged)
            {
                Context.CommandManager.InvokeCommand(new ExitSettingsCommand());
                //禁用界面
                _view.gameObject.SetActive(false);
            }
            else
            {
                string message = _service.GetLocaleString("Warning_SaveSettings");

                void ConfirmAction()
                {
                    Context.CommandManager.InvokeCommand(new ExitSettingsCommand());
                    _view.gameObject.SetActive(false);
                }

                void CancelAction()
                {
                    _service.LoadGameSettings();
                    Context.CommandManager.InvokeCommand(new ExitSettingsCommand());
                    _view.gameObject.SetActive(false);
                }

                //弹窗提示没有保存
                Context.CommandManager.InvokeCommand(new OpenCheckBoxCommand
                {
                    Message = message, ConfirmCallback = ConfirmAction, CancelCallback = CancelAction
                });
            }
        }

        private void OnApplyButtonPressed()
        {
            _service.SaveGameSettings();
            _model.SettingChanged = false;
            _view.ApplyButton.gameObject.SetActive(false);
        }
    }
}