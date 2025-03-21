﻿using NaughtyAttributes.Scripts.Core.Utility;
using System;

namespace NaughtyAttributes.Scripts.Core.MetaAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class DisableIfAttribute : EnableIfAttributeBase
    {
        public DisableIfAttribute(string condition)
            : base(condition)
        {
            Inverted = true;
        }

        public DisableIfAttribute(EConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions)
        {
            Inverted = true;
        }

        public DisableIfAttribute(string enumName, object enumValue)
            : base(enumName, enumValue as Enum)
        {
            Inverted = true;
        }
    }
}
