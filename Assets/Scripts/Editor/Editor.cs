//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    public abstract class Editor<T> : Editor where T : Object
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                OnInspectorGUI((T)target);
            }
        }

        protected abstract void OnInspectorGUI(T target);
    }
}
