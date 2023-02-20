//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(LevelBuilder))]
    public sealed class LevelBuilderEditor : Editor<LevelBuilder>
    {
        protected override void OnInspectorGUI(LevelBuilder target)
        {
            if (GUILayout.Button("Next"))
            {
                target.Next();
            }
        }
    }
}
