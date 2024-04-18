using System.Collections.Generic;
using UnityEngine;
using static ZiercCode.Test.解释器模式.Instruction;

namespace ZiercCode.Test.解释器模式
{
    //小虚拟机
    public class ZiercVM
    {
        /// <summary>
        /// 储存指令字节码，用于后续执行
        /// </summary>
        private Stack<int> _stack;

        public ZiercVM()
        {
            _stack = new Stack<int>();
        }
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="value">字节码</param>
        private void Push(int value)
        {
            if (_stack != null)
            {
                _stack.Push(value);
            }
        }
        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns>指令字节码</returns>
        private int Pop()
        {
            if (_stack != null && _stack.Count > 0)
            {
                return _stack.Pop();
            }
            Debug.LogError("栈不存在或栈为空");
            return -1;
        }

        /// <summary>
        /// 解释器
        /// </summary>
        public void interpret(int[] bytecode)
        {
            for (int i = 0; i < bytecode.Length; i++)
            {
                Instruction instruction = (Instruction)bytecode[i];
                switch (instruction)
                {
                    case Z_LITERAL:
                    Push(bytecode[++i]);
                    break;
                    case Z_GET_HEALTH:

                    break;
                    case Z_SET_HEALTH:
                    Debug.Log("设置健康值");
                    break;
                    default:
                    Debug.Log("指令不存在");
                    break;
                }
            }

        }

    }
}
