using RMC.Mini;
using RMC.Mini.Controller;
using Unity.VisualScripting;

namespace ZiercCode.Test.MVC
{
    public class CheckBoxController : BaseController<CheckBoxModel, CheckBoxView, Null>
    {
        public CheckBoxController(CheckBoxModel model, CheckBoxView view) : base(model, view, null)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.ConfirmButton.onClick.AddListener(View_OnConfirmButtonClicked);
                _view.CancelButton.onClick.AddListener(View_OnCancelButtonClicked);

                _model.MessageText.AddListener(Model_OnMessageTextChanged);

                Context.CommandManager.AddCommandListener<OpenCheckBoxCommand>(OnOpenCheckBox);
            }
        }

        private void Show()
        {
            _view.gameObject.SetActive(true);
        }

        private void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        private void Enable()
        {
            _view.CanvasGroup.alpha = 1;
            _view.CanvasGroup.interactable = true;
            _view.CanvasGroup.blocksRaycasts = true;
        }

        private void Disable()
        {
            _view.CanvasGroup.alpha = 0.3f;
            _view.CanvasGroup.interactable = false;
            _view.CanvasGroup.blocksRaycasts = false;
        }

        private void OnOpenCheckBox(OpenCheckBoxCommand openCheckBoxCommand)
        {
            _model.MessageText.Value = openCheckBoxCommand.Message;
            _model.CancelAction.Value = openCheckBoxCommand.CancelCallback;
            _model.ConfirmAction.Value = openCheckBoxCommand.ConfirmCallback;


            Show();
            Enable();
        }

        private void View_OnCancelButtonClicked()
        {
            _model.CancelAction.Value.Invoke();

            Disable();
            Hide();
        }

        private void View_OnConfirmButtonClicked()
        {
            _model.ConfirmAction.Value.Invoke();

            Disable();
            Hide();
        }

        private void Model_OnMessageTextChanged(string msg)
        {
            _view.MessageText.text = msg;
        }
    }
}