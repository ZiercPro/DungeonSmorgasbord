using RMC.Core.Observables;
using RMC.Mini;
using RMC.Mini.Controller;
using System;
using UnityEngine.Events;

namespace ZiercCode._DungeonGame.UI.CheckBox
{
    public class CheckBoxController : BaseController<CheckBoxModel, CheckBoxView, CheckBoxService>
    {
        public CheckBoxController(CheckBoxModel model, CheckBoxView view, CheckBoxService service) : base(model, view,
            service)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);
                _view.CancelButton.onClick.AddListener(OnCancelButtonClicked);

                BindData(_model.MessageText, v => _view.MessageText.text = v, null);

                Context.CommandManager.AddCommandListener<OpenCheckBoxCommand>(OnOpenCheckBox);

                _service.OnBackInput.AddListener(OnCancelButtonClicked);
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
            });
        }

        private void OnOpenCheckBox(OpenCheckBoxCommand openCheckBoxCommand)
        {
            _model.MessageText.Value = openCheckBoxCommand.Message;
            _model.CancelAction = openCheckBoxCommand.CancelCallback;
            _model.ConfirmAction = openCheckBoxCommand.ConfirmCallback;

            _service.SetUIInput(true); //开启快捷键监听
            _view.gameObject.SetActive(true);
            _view.CanvasGroupUser.Enable();
        }

        private void OnCancelButtonClicked()
        {
            _model.CancelAction?.Invoke();

            _service.SetUIInput(false);
            _view.CanvasGroupUser.Disable();
            _view.gameObject.SetActive(false);
        }

        private void OnConfirmButtonClicked()
        {
            _model.ConfirmAction?.Invoke();

            _service.SetUIInput(false);
            _view.CanvasGroupUser.Disable();
            _view.gameObject.SetActive(false);
        }
    }
}