using UnityEditor;

namespace com.absence.attributes.editor
{
    /// <summary>
    /// The static class responsible for handling needed backend stuff in the editor.
    /// </summary>
    public static class EditorJobsHelper
    {
        [InitializeOnLoadMethod]
        static void RefreshFieldButtonDatabase()
        {
            FieldButtonManager.Refresh();
        }
    }
}
