using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZRuntime
{

    public class Interactiveable : MonoBehaviour
    {
        [SerializeField] private Vector3 uiPosition;
        [SerializeField] private float activeRadius;
        [SerializeField] private GameObject buttonUITemp;

        private InteractHandler _interactHandler;
        private InterActiveButtonUI _keyBoard;
        private GameObject _buttonUI;
        public UnityEvent onActive;

        private Action OnActive;

        private void Awake()
        {
            _interactHandler = null;
            _buttonUI = Instantiate(buttonUITemp, transform);
            _keyBoard = _buttonUI.GetComponent<InterActiveButtonUI>();
        }

        private void Start()
        {
            _buttonUI.transform.localPosition = uiPosition;
            OnActive = () => { onActive?.Invoke(); };
            ButtonUIHide();
        }

        private void OnDisable()
        {
            ButtonUIHide();
        }

        private void Update()
        {
            DetectPlayer();
        }

        public void ButtonUIShow()
        {
            _keyBoard.gameObject.SetActive(true);
            _keyBoard.ShowText();
        }

        public void ButtonUIHide()
        {
            _keyBoard.gameObject.SetActive(false);
        }

        public void InterAct(HotKey pressKey)
        {
            if (pressKey == _keyBoard.hotkey)
                onActive?.Invoke();
        }

        //检测附近是否有可以互动的玩家
        //检测到了就显示ui
        private void DetectPlayer()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, activeRadius);
            foreach (var c2d in collider2Ds)
            {
                InteractHandler temp = c2d.GetComponent<InteractHandler>();
                if (temp)
                {
                    _interactHandler = temp;
                    {
                        if (!_keyBoard.gameObject.activeInHierarchy)
                        {
                            ButtonUIShow();
                            _interactHandler.AddToEvent(OnActive);
                        }

                        return;
                    }
                }

                if (_keyBoard.gameObject.activeInHierarchy)
                {
                    ButtonUIHide();
                    if (_interactHandler)
                    {
                        _interactHandler.RemoveFromEvent(OnActive);
                    }
                }
            }
        }
    }
}