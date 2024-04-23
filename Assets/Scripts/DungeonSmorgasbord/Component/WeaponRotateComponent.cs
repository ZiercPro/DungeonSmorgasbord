using NaughtyAttributes;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    /// <summary>
    /// 武器旋转组件，用于实现武器的旋转
    /// </summary>
    public class WeaponRotateComponent : MonoBehaviour
    {
        /// <summary>
        /// 旋转锚点
        /// </summary>
        [SerializeField] private Transform rotationAnchorPosition;

        /// <summary>
        /// 武器固定锚点
        /// </summary>
        [SerializeField] private Transform handPosition;

        /// <summary>
        /// 角色渲染组件
        /// </summary>
        [SerializeField] private SpriteRenderer characterRenderer;

        /// <summary>
        /// 是否可以更换武器
        /// </summary>
        [SerializeField, Tooltip("是否可以更换武器")] private bool canChangeWeapon;

        /// <summary>
        /// 武器渲染组件
        /// </summary>
        [SerializeField, HideIf("canChangeWeapon")]
        private SpriteRenderer weaponRenderer;

        /// <summary>
        /// 自动翻转组件
        /// </summary>
        [SerializeField] private AutoFlipComponent autoFlipComponent;
        
        
        private Transform _weaponTransform;


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
            Vector2 myPos = rotationAnchorPosition.position;
            float reRotation = Mathf.Atan2(viewPos.y - myPos.y, viewPos.x - myPos.x) * Mathf.Rad2Deg;
            Vector3 targetAngles = new Vector3(0, 0, reRotation);
            rotationAnchorPosition.eulerAngles = targetAngles;

            //翻转
            if (!autoFlipComponent.IsFacingRight)
                rotationAnchorPosition.localScale = new Vector3(1, -1, 1);
            else if (autoFlipComponent.IsFacingRight)
                rotationAnchorPosition.localScale = new Vector3(1, 1, 1);

            //图层
            if (rotationAnchorPosition.rotation.z > 0 && rotationAnchorPosition.rotation.z < 180)
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
            else weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
}