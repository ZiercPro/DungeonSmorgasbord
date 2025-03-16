using System;
using System.Collections.Generic;

namespace ZiercCode.Utilities
{
    public static class TypeTool
    {
        /// <summary>
        /// 获取抽象基类数组
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <returns>目标类型所有的抽象基类</returns>
        public static Type[] GetAbstractTypes(Type type)
        {
            List<Type> absTypes = new();
            Type baseType = type.BaseType;
            while (baseType != null)
            {
                if (baseType.IsAbstract) absTypes.Add(baseType);
                baseType = baseType.BaseType;
            }

            return absTypes.ToArray();
        }
    }
}