using com.absence.editor.gui;
using com.absence.editor.internals;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomPropertyDrawer(typeof(LineAttribute))]
    public class LineDecoratorDrawer : DecoratorDrawer
    {
        LineAttribute line => (attribute as LineAttribute);

        public override float GetHeight()
        {
            return Constants.LINE_MARGINS + Constants.LINE_HEIGHT;
        }

        public override void OnGUI(Rect position)
        {
            if (line.colorSet) absentGUI.DrawLine(position, line.color);
            else absentGUI.DrawLine(position);
        }
    }
}
