using System;
using UnityEngine;
using UnityEngine.Events;
using ZiercCode.DungeonSmorgasbord.UI;

namespace ZiercCode.DungeonSmorgasbord.Extend
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Interactive : MonoBehaviour
    {
        [SerializeField] private GameObject buttonUITemp;
        [SerializeField] private UnityEvent onActive;
        [SerializeField] private Vector3 uiPosition;

        private InteractHandler _interactHandler;
        private InterActiveButtonUI _keyBoard;
        private Coroutine _detectCoroutine;
        private bool _isHandlerExist;
        private GameObject _buttonUI;

        private Action _onActive;

        private void Awake()
        {
            _interactHandler = null;
            _buttonUI = Instantiate(buttonUITemp, transform);
            _keyBoard = _buttonUI.GetComponent<InterActiveButtonUI>();
        }

        private void Start()
        {
            _buttonUI.transform.localPosition = uiPosition;
            _onActive = () => { onActive?.Invoke(); };
            ButtonUIHide();
        }

        private void OnDisable()
        {
            ButtonUIHide();
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            InteractHandler temp = other.GetComponent<InteractHandler>();
            if (temp)
            {
                _interactHandler = temp;
                {
                    if (!_keyBoard.gameObject.activeSelf)
                    {
                        ButtonUIShow();
                        _interactHandler.AddToEvent(_onActive);
                    }
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_interactHandler)
            {
                if (!_keyBoard.gameObject.activeSelf)
                {
                    ButtonUIShow();
                    _interactHandler.RemoveFromEvent(_onActive);
                    _interactHandler.AddToEvent(_onActive);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_keyBoard.gameObject.activeInHierarchy)
            {
                ButtonUIHide();
                if (_interactHandler)
                {
                    _interactHandler.RemoveFromEvent(_onActive);
                    _interactHandler = null;
                }
            }
        }
    }
}