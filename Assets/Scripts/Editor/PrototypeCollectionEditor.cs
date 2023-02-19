//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(PrototypeCollection))]
    public class PrototypeCollectionEditor : Editor<PrototypeCollection>
    {
        protected override void OnInspectorGUI(PrototypeCollection target)
        {
            if (GUILayout.Button("Create modules"))
            {
                var prototypes = target.prototypes;

                foreach (var prototype in prototypes)
                {
                    var slot = prototype.slot;
                    for (int i = 0; i < slot.targetSlotTypes.Length; i++)
                    {
                        var position = target.positions[i];
                        var rotation = target.rotations[i];

                        var go = new GameObject(prototype.name);
                        var module = go.AddComponent<Module>();
                        var parent = go.transform;

                        var clone = Object.Instantiate(prototype, position, rotation, parent);
                        Object.DestroyImmediate(clone);

                        PrefabUtility.SaveAsPrefabAsset(go, $"Prefabs/Modules/{prototype.name}{i}.prefab");
                        Object.DestroyImmediate(go);
                    }
                }
            }
        }
    }
}
