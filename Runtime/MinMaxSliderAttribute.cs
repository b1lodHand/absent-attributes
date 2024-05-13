using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class MinMaxSliderAttribute : PropertyAttribute
    {
        public float min { get; private set; }
        public float max { get; private set; }

        public MinMaxSliderAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
