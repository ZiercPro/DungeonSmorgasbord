using UnityEngine;
using ZiercCode.Old.Component;

namespace ZiercCode.Old.Weapon
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private Transform handPosition;
        [SerializeField] private SpriteRenderer characterRenderer;

        private Transform _weaponTransform;
        private FlipController _flipController;
        private SpriteRenderer _weaponRenderer;

        private void Awake()
        {
            _flipController = GetComponent<FlipController>();
        }

        public void SetWeaponParent(Transform weaponTransform)
        {
            _weaponTransform = weaponTransform;
            _weaponTransform.SetParent(handPosition);
            _weaponTransform.localScale = Vector3.one;
            _weaponTransform.localPosition = Vector3.zero;
            _weaponTransform.localRotation = Quaternion.identity;
        }

        public void WeaponRotateTo(Vector2 viewPos)
        {
            //指向指针
            Vector2 myPos = transform.position;
            float reRotation = Mathf.Atan2(viewPos.y - myPos.y, viewPos.x - myPos.x) * Mathf.Rad2Deg;
            Vector3 targetAngles = new Vector3(0, 0, reRotation);
            transform.eulerAngles = targetAngles;

            //翻转
            if (!_flipController.isFaceingRight)
                transform.localScale = new Vector3(1, -1, 1);
            else if (_flipController.isFaceingRight)
                transform.localScale = new Vector3(1, 1, 1);

            //图层
            if (transform.rotation.z > 0 && transform.rotation.z < 180)
                _weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
            else _weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
}