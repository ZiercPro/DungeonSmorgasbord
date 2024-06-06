using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZiercCode.Test.MVC
{
    public class AddCalculatorView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_InputField aInputField;
        [SerializeField] private TMP_InputField bInputField;
        [SerializeField] private TMP_InputField resultInputField;
        [SerializeField] private Button addButton;
        [SerializeField] private Button resetButton;

        public UnityEvent OnAddButtonPressed;
        public UnityEvent OnResetButtonPressed;

        public void RequireIsInitialized()
        {
            if (!_isInitialize)
                Debug.LogWarning($"{name}必须初始化");
        }

        public bool IsInitialized => _isInitialize;
        public IContext Context => _context;

        private bool _isInitialize;
        private IContext _context;

        public void Initialize(IContext context)
        {
            _context = context;


            OnAddButtonPressed = new UnityEvent();
            OnResetButtonPressed = new UnityEvent();

            //按钮点击事件绑定
            addButton.onClick.AddListener(PressAdd);
            resetButton.onClick.AddListener(PressReset);

            //获取加法数据模型
            AddCalculatorModel addCalculatorModel =
                _context.ModelLocator.GetItem<AddCalculatorModel>();

            //绑定数据改变事件
            addCalculatorModel.A.AddListener(OnAValueChange);
            addCalculatorModel.B.AddListener(OnBValueChange);
            addCalculatorModel.Result.AddListener(OnResultValueChange);

            _isInitialize = true;
        }


        public string GetAInputFieldText()
        {
            return aInputField.text;
        }

        public string GetBInputFieldText()
        {
            return bInputField.text;
        }

        private void PressAdd()
        {
            OnAddButtonPressed?.Invoke();
        }

        private void PressReset()
        {
            OnResetButtonPressed?.Invoke();
        }

        private void OnAValueChange(int x)
        {
            aInputField.text = x.ToString();
        }

        private void OnBValueChange(int x)
        {
            bInputField.text = x.ToString();
        }

        private void OnResultValueChange(int x)
        {
            resultInputField.text = x.ToString();
        }
    }
}