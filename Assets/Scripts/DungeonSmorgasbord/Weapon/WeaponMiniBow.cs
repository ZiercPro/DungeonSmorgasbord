using DG.Tweening;
using System.Collections;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponMiniBow : WeaponBase
    {
        [SerializeField] private WeaponDataSo arrowDataSo;


        private GameObject _arrow;
        private Coroutine _shake;
        private Coroutine _holdTimer;
        private Coroutine _changeArrowSpriteRender;
        private bool _isHolding;
        private float _holdingTime;

        public override void OnLeftButtonPressStarted()
        {
            AttackStart();
        }

        public override void OnLeftButtonPressed()
        {
            _holdTimer = StartCoroutine(HoldTimer());
            //  _changeArrowSpriteRender = StartCoroutine(UpdateArrowSpriteSort());
            // _shake = StartCoroutine(Shake());
        }

        public override void OnLeftButtonPressCanceled()
        {
            AttackEnd();
        }

        private void AttackStart()
        {
            int holding = Animator.StringToHash("isHolding");
            _isHolding = true;
            //  animator.SetBool(holding, _isHolding);

            HoldArrow();
        }

        private void HoldArrow()
        {
            Debug.Log("is hold");
            _arrow = Instantiate(arrowDataSo.prefab, transform);
            _arrow.transform.localPosition = Vector3.zero;
            _arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }

        private void ReleaseArrow()
        {
            _arrow.transform.SetParent(null);
        }

        private void AttackEnd()
        {
            int holding = Animator.StringToHash("isHolding");
            _isHolding = false;
            //animator.SetBool(holding, _isHolding);
            //StopCoroutine(_changeArrowSpriteRender);
            StopCoroutine(_holdTimer);
            //StopCoroutine(_shake);
            ReleaseArrow();
        }

        private IEnumerator Shake()
        {
            float maxShakeStrength = 0.2f;
            float currentShakeStrength = 0f;
            while (_isHolding)
            {
                if (_holdingTime / 20 < maxShakeStrength)
                    currentShakeStrength = _holdingTime / 20;
                transform.DOShakePosition(Time.deltaTime, currentShakeStrength);
                yield return null;
            }
        }

        private IEnumerator HoldTimer()
        {
            _holdingTime = 0f;
            while (_isHolding)
            {
                _holdingTime += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator UpdateArrowSpriteSort()
        {
            if (!_arrow) yield break;
            SpriteRenderer arrowRenderer = _arrow.GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer weaponRenderer = GetComponentInChildren<SpriteRenderer>();
            while (_isHolding)
            {
                if (arrowRenderer.sortingOrder <= weaponRenderer.sortingOrder)
                    arrowRenderer.sortingOrder += weaponRenderer.sortingOrder + 1;
                yield return null;
            }
        }
    }
}