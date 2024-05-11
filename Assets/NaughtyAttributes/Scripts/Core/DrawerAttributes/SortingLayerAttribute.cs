using System;

namespace NaughtyAttributes.Scripts.Core.DrawerAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SortingLayerAttribute : DrawerAttribute
    {
    }
}