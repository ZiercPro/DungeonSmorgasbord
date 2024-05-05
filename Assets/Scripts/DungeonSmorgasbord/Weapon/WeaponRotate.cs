using NaughtyAttributes;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器旋转组件，用于实现武器的旋转，瞄准鼠标位置
    /// </summary>
    public class WeaponRotate : MonoBehaviour
    {
        /// <summary>
        /// 武器旋转锚点
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
        /// 自动翻转组件
        /// </summary>
        [SerializeField] private AutoFlipComponent autoFlipComponent;

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
        /// 武器transform
        /// </summary>
        [SerializeField, HideIf("canChangeWeapon")]
        private Transform weaponTransform;

        /// <summary>
        /// 是否能旋转，用于武器切换时使用，因为需要调用设置方法，不能直接禁用组件
        /// </summary>
        [SerializeField] private bool canRotate;


        /// <summary>
        /// 设置武器的父物体
        /// </summary>
        /// <param name="weaponSpriteRenderer">武器渲染组件</param>
        /// <param name="weaponTransform">武器的transform组件</param>
        public void SetWeapon(Transform weaponTransform, SpriteRenderer weaponSpriteRenderer)
        {
            canRotate = false;
            weaponRenderer = weaponSpriteRenderer;
            this.weaponTransform = weaponTransform;
            weaponTransform.SetParent(handPosition);
            weaponTransform.localScale = Vector3.one;
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.localRotation = Quaternion.identity;
            canRotate = true;
        }

        /// <summary>
        /// 武器旋转目标位置
        /// </summary>
        /// <param name="viewPos">目标位置</param>
        public void WeaponRotateTo(Vector2 viewPos)
        {
            if (!canRotate) return;
            //指向指针
            Vector2 direction = viewPos - (Vector2)rotationAnchorPosition.position;
            rotationAnchorPosition.right = direction;
            // Vector2 myPos = rotationAnchorPosition.position;
            // float reRotation = Mathf.Atan2(viewPos.y - myPos.y, viewPos.x - myPos.x) * Mathf.Rad2Deg;
            // Vector3 targetAngles = new Vector3(0, 0, reRotation);
            // rotationAnchorPosition.localEulerAngles = targetAngles;


            //翻转
            if (!autoFlipComponent.IsFacingRight)
                handPosition.localScale = new Vector3(1, -1, 1);
            else if (autoFlipComponent.IsFacingRight)
                handPosition.localScale = new Vector3(1, 1, 1);

            //图层
            if (rotationAnchorPosition.rotation.z > 0 && rotationAnchorPosition.rotation.z < 180)
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
            else weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
}