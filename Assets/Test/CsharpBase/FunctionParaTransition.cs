using UnityEngine;

namespace ZiercCode.Test.CsharpBase
{
    /// <summary>
    /// 测试函数参数传递的底层逻辑
    /// </summary>
    public class FunctionParaTransition : MonoBehaviour
    {
        private void Start()
        {
            Test();
        }

        public void Test()
        {
            // Bird birdA = new Bird("Sandy", 10f, 50f);
            // CookBird(birdA);
            // Debug.Log($"{birdA.Name},{birdA.Speed},{birdA.Weight}");
            //
            // MyVector2 newVector2;
            // newVector2.X = 2f;
            // newVector2.Y = 2f;
            // ChangeVector2(ref newVector2);
            // Debug.Log($"{newVector2.X},{newVector2.Y}");
        }

        public void CookBird(Bird target)
        {
            target.Name += " Cooked";
            target.Speed = 0f;
            target.Weight = 0f;
        }

        public void ChangeVector2(ref MyVector2 target)
        {
            target.X += 1.0f;
            target.Y += 1.0f;
        }

        public void ChangeString(ref string target)
        {
            target += " Changed";
        }
    }

    public class Bird
    {
        public string Name;
        public float Speed;
        public float Weight;

        public Bird(string name, float speed, float weight)
        {
            Name = name;
            Speed = speed;
            Weight = weight;
        }
    }

    public struct MyVector2
    {
        public float X;
        public float Y;
    }
}