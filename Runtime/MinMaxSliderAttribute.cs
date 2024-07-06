using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a slider with two knobs that represents x and y values of a <see cref="Vector3"/> or <see cref="Vector3Int"/>.
    /// </summary>
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
