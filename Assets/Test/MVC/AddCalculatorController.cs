using RMC.Mini;
using RMC.Mini.Controller;
using System;
using ZiercCode.Test.MVC.Scene;

namespace ZiercCode.Test.MVC
{
    public class AddCalculatorController : BaseController<AddCalculatorModel, AddCalculatorView, AddCalculatorService>
    {
        private readonly AddCalculatorModel _addCalculatorModel;
        private readonly AddCalculatorView _addCalculatorView;

        public AddCalculatorController(AddCalculatorModel addCalculatorModel,
            AddCalculatorView addCalculatorView
        ) : base(addCalculatorModel,
            addCalculatorView,
            null)
        {
            _addCalculatorModel = addCalculatorModel;
            _addCalculatorView = addCalculatorView;
        }

        public override void Initialize(IContext context)
        {
            base.Initialize(context);

            _addCalculatorView.OnAddButtonPressed.AddListener(View_OnAdd);
            _addCalculatorView.OnResetButtonPressed.AddListener(View_OnReset);
        }

        private void View_OnAdd()
        {
            if (Int32.TryParse(_addCalculatorView.GetAInputFieldText(), out int a))
            {
                _addCalculatorModel.A.Value = a;
            }

            if (Int32.TryParse(_addCalculatorView.GetBInputFieldText(), out int b))
            {
                _addCalculatorModel.B.Value = b;
            }

            _addCalculatorModel.Result.Value = a + b;
        }

        private void View_OnReset()
        {
            _addCalculatorModel.A.Value = 0;
            _addCalculatorModel.B.Value = 0;
            _addCalculatorModel.Result.Value = 0;
        }
    }
}