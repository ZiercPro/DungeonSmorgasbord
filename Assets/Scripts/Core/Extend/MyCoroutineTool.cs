using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode.Core.Extend
{
    /// <summary>
    /// 帮忙开启协程的单例
    /// </summary>
    public class MyCoroutineTool : USingleton<MyCoroutineTool>
    {
        private List<Coroutine> _myCoroutines;

        protected override void Awake()
        {
            base.Awake();
            _myCoroutines = new List<Coroutine>();
        }

        private void OnDisable()
        {
            StopAllCor();
        }

        /// <summary>
        /// 开启一个协程
        /// </summary>
        public Coroutine StartMyCor(IEnumerator cor)
        {
            Coroutine newCor = StartCoroutine(cor);
            _myCoroutines.Add(newCor);
            return newCor;
        }

        /// <summary>
        /// 关闭一个协程
        /// </summary>
        public void StopMyCor(Coroutine cor)
        {
            StopCoroutine(cor);
            _myCoroutines.Remove(cor);
        }

        /// <summary>
        /// 关闭所有协程
        /// </summary>
        public void StopAllCor()
        {
            StopAllCoroutines();
            if (_myCoroutines != null && _myCoroutines.Count > 0)
                MyMath.ForeachFromLast(_myCoroutines, StopCoroutine);
        }
    }
}