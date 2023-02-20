//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(PrototypeCollection))]
    public sealed class PrototypeCollectionEditor : Editor<PrototypeCollection>
    {
        protected override void OnInspectorGUI(PrototypeCollection target)
        {
            if (GUILayout.Button("Discover prototypes"))
            {
                var guids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Prefabs/Prototypes" });
                var prototypes = new List<Prototype>();
                foreach (var guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var prototype = AssetDatabase.LoadAssetAtPath<Prototype>(path);
                    prototypes.Add(prototype);
                }

                target.prototypes = prototypes.ToArray();
                EditorUtility.SetDirty(target);
            }
        }
    }
}
