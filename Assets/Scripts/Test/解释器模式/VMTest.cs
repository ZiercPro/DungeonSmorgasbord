using UnityEngine;

namespace ZiercCode.Test.解释器模式
{
    public class VMTest : MonoBehaviour
    {
        private ZiercVM _ziercVM;
        private int[] instructions;
        // Start is called before the first frame update
        void Start()
        {
            _ziercVM = new ZiercVM();
            instructions = new int[] { 1, 2, 3, 2, 3 };
            _ziercVM.interpret(instructions);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
