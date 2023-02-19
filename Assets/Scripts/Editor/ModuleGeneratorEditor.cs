//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(ModuleGenerator))]
    public class ModuleGeneratorEditor : Editor<ModuleGenerator>
    {
        protected override void OnInspectorGUI(ModuleGenerator target)
        {
            if (GUILayout.Button("Create modules"))
            {
                foreach (var slotType in target.slotTypes)
                {
                    slotType.modules.Clear();
                    EditorUtility.SetDirty(slotType);
                }

                foreach (var prototype in target.prototypeCollection.prototypes)
                {
                    var slot = prototype.slot;
                    for (int i = 0; i < slot.targetSlotTypes.Length; i++)
                    {
                        var targetSlotType = slot.targetSlotTypes[i];
                        var position = target.positions[i];
                        var rotation = target.rotations[i];

                        var go = new GameObject(prototype.name);
                        var module = go.AddComponent<Module>();
                        var parent = go.transform;

                        // TODO Jonas: implement allowed neighbors

                        var clone = Object.Instantiate(prototype, position, rotation, parent);
                        clone.name = prototype.name;
                        Object.DestroyImmediate(clone);

                        var prefab = PrefabUtility.SaveAsPrefabAsset(go, $"Assets/Prefabs/Modules/{prototype.name}{i}.prefab");
                        Object.DestroyImmediate(go);

                        var prefabModule = prefab.GetComponent<Module>();
                        targetSlotType.modules.Add(prefabModule);
                    }
                }

                AssetDatabase.SaveAssets();
            }
        }
    }
}
