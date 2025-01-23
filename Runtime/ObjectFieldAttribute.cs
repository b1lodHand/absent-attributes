using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a big square field for non-scene object fields that derive from <see cref="UnityEngine.Object"/>.
    /// </summary>
    public class ObjectFieldAttribute : PropertyAttribute
    {
        public Type type;
        public bool allowSceneObjects;

        public virtual bool AllowSceneObjects => allowSceneObjects;

        public ObjectFieldAttribute(Type type)
        {
            this.type = type;
            this.allowSceneObjects = false;
        }
    }
}
