using UnityEngine;

namespace ZiercCode.GameTools_2D
{
    [RequireComponent(typeof(RotateToCursor))]
    public class FlipWithRotateToCursor : MonoBehaviour //为了优化直接旋转的2d图片超过180°后的表现错误
    {
        [SerializeField] private bool defaultRight; //有些材质默认朝向左边 那就需要与默认朝右边的操作相反

        private void Update()
        {
            Flip();
        }

        private void Flip()
        {
            if (transform.rotation.eulerAngles.z > 90f &&
                transform.rotation.eulerAngles.z < 270f) //在这里欧拉角的范围是0-360 在检视窗口显示的范围是0~180/-180
            {
                if (defaultRight)
                    transform.localScale = new Vector3(1f, -1f, 1f);
                else
                    transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                if (defaultRight)
                    transform.localScale = new Vector3(1f, 1f, 1f);
                else
                    transform.localScale = new Vector3(1f, -1f, 1f);
            }
        }
    }
}