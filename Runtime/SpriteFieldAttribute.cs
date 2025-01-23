using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Wrapper class for big 'n square sprite fields. Equivalent of:
    /// <code>
    /// [ObjectField(typeof(Sprite), allowSceneObjects = false)]
    /// </code>
    /// </summary>
    public class SpriteFieldAttribute : ObjectFieldAttribute
    {
        public override bool AllowSceneObjects => false;

        public SpriteFieldAttribute() : base(typeof(Sprite))
        {
        }
    }
}
