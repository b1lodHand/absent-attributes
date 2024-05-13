using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderPropertyDrawer : PropertyDrawer
    {
        MinMaxSliderAttribute m_minMax => attribute as MinMaxSliderAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var startRect = EditorGUI.PrefixLabel(position, label);

            var fixedFieldSize = 50f;
            var spacing = 5f;
            var sliderSize = startRect.width - (2 * (fixedFieldSize + spacing));

            var tempMin = 0f;
            var tempMax = 0f;

            if(property.propertyType == SerializedPropertyType.Vector2)
            {
                tempMin = property.vector2Value.x;
                tempMax = property.vector2Value.y;
            }

            else if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                tempMin = property.vector2IntValue.x;
                tempMax = property.vector2IntValue.y;
            }

            startRect.width = fixedFieldSize;

            EditorGUI.BeginChangeCheck();

            EditorGUI.FloatField(startRect, tempMin);

            startRect.x += spacing;
            startRect.x += fixedFieldSize;
            startRect.width = sliderSize;

            EditorGUI.MinMaxSlider(startRect, ref tempMin, ref tempMax, m_minMax.min, m_minMax.max);

            startRect.x += spacing;
            startRect.x += sliderSize;
            startRect.width = fixedFieldSize;

            EditorGUI.FloatField(startRect, tempMax);

            if (tempMin < m_minMax.min) tempMin = m_minMax.min;
            else if (tempMin > tempMax) tempMin = tempMax;

            if(tempMax > m_minMax.max) tempMax = m_minMax.max;
            else if (tempMax < tempMin) tempMax = tempMin;

            if (EditorGUI.EndChangeCheck())
            {
                if (property.propertyType == SerializedPropertyType.Vector2)
                {
                    Vector2 result = new Vector2(tempMin, tempMax);
                    property.vector2Value = result;
                }

                else if (property.propertyType == SerializedPropertyType.Vector2Int)
                {
                    Vector2Int result = new Vector2Int(Mathf.FloorToInt(tempMin), Mathf.FloorToInt(tempMax));
                    property.vector2IntValue = result;
                }
            }
        }
    }
}
