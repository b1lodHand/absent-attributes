using com.absence.attributes.internals;
using com.absence.editor.gui;
using System;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomPropertyDrawer(typeof(BaseHeaderAttribute), true)]
    public class BaseHeaderDecoratorDrawer : DecoratorDrawer
    {
        BaseHeaderAttribute header => (attribute as BaseHeaderAttribute);

        public override float GetHeight()
        {
            if (header == null) return base.GetHeight();

            return CalcHeight();
        }

        public override void OnGUI(Rect position)
        {
            if (header == null) return;

            switch (header.headerType)
            {
                case BaseHeaderAttribute.HeaderType.H1:
                    absentGUI.Header1(position, header.headerText);
                    break;

                case BaseHeaderAttribute.HeaderType.H2:
                    absentGUI.Header2(position, header.headerText);
                    break;

                case BaseHeaderAttribute.HeaderType.H3:
                    absentGUI.Header3(position, header.headerText);
                    break;

                default:
                    break;
            }
        }

        private float CalcHeight()
        {
            GUIContent tempContent = new GUIContent(header.headerText);
            GUIStyle style = FindStyle();
            RectOffset margin = style.margin;
            RectOffset padding = style.padding;

            const int horizontalOffset = 10;
            float currentWidth = EditorGUIUtility.currentViewWidth - (margin.left + margin.right) - (padding.left + padding.right) - horizontalOffset;

            return style.CalcHeight(tempContent, currentWidth) + margin.bottom;
        }

        private GUIStyle FindStyle()
        {
            return header.headerType switch
            {
                BaseHeaderAttribute.HeaderType.H1 => absentEditorStyles.Header1Style,
                BaseHeaderAttribute.HeaderType.H2 => absentEditorStyles.Header2Style,
                BaseHeaderAttribute.HeaderType.H3 => absentEditorStyles.Header3Style,
                _ => null,
            };
        }
    }
}
