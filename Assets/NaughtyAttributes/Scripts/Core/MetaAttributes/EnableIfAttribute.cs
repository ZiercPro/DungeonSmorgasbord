﻿using NaughtyAttributes.Scripts.Core.Utility;
using System;

namespace NaughtyAttributes.Scripts.Core.MetaAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class EnableIfAttribute : EnableIfAttributeBase
    {
        public EnableIfAttribute(string condition)
            : base(condition)
        {
            Inverted = false;
        }

        public EnableIfAttribute(EConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions)
        {
            Inverted = false;
        }

        public EnableIfAttribute(string enumName, object enumValue)
            : base(enumName, enumValue as Enum)
        {
            Inverted = false;
        }
    }
}
