using RMC.Mini;
using UnityEngine;

namespace ZiercCode.Test.MVC
{
    public class AddCalculatorTest : MonoBehaviour
    {
        [SerializeField] private AddCalculatorView addCalculatorView;

        private IContext _context;
        private AddCalculatorModel _addCalculatorModel;
        private AddCalculatorController _addCalculatorController;

        private void Awake()
        {
            _context = new Context();
            _addCalculatorModel = new AddCalculatorModel();
            _addCalculatorController = new AddCalculatorController(_addCalculatorModel, addCalculatorView);
        }

        private void Start()
        {
            _addCalculatorModel.Initialize(_context);
            addCalculatorView.Initialize(_context);
            _addCalculatorController.Initialize(_context);
        }
    }
}