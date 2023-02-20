//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//---------------------------------------- 
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(ModuleGenerator))]
    public class ModuleGeneratorEditor : Editor<ModuleGenerator>
    {
        private static readonly Direction[] directions =
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.Up,
            Direction.Down,
        };

        private static readonly Direction[] inverseDirections =
        {
            Direction.South,
            Direction.North,
            Direction.West,
            Direction.East,
            Direction.Down,
            Direction.Up,
        };

        private static readonly Direction[,] rotatedDirections =
        {
            {Direction.North, Direction.South, Direction.East, Direction.West, Direction.Up, Direction.Down},
            {Direction.West, Direction.East, Direction.South, Direction.North, Direction.Up, Direction.Down},
            {Direction.South, Direction.North, Direction.West, Direction.East, Direction.Up, Direction.Down},
            {Direction.East, Direction.West, Direction.North, Direction.South, Direction.Up, Direction.Down},
        };

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
                        module.id = targetSlotType.modules.Count;
                        module.prototype = prototype;

                        var parent = go.transform;

                        var clone = Object.Instantiate(prototype, position, rotation, parent);
                        clone.name = prototype.name;
                        Object.DestroyImmediate(clone);

                        var prefab = PrefabUtility.SaveAsPrefabAsset(go, $"Assets/Prefabs/Modules/{prototype.name}{i}.prefab");
                        Object.DestroyImmediate(go);

                        var prefabModule = prefab.GetComponent<Module>();
                        targetSlotType.modules.Add(prefabModule);
                    }
                }

                foreach (var prototype in target.prototypeCollection.prototypes)
                {
                    var slot = prototype.slot;
                    for (int i = 0; i < slot.targetSlotTypes.Length; i++)
                    {
                        var targetSlotType = slot.targetSlotTypes[i];
                        var module = AssetDatabase.LoadAssetAtPath<Module>($"Assets/Prefabs/Modules/{prototype.name}{i}.prefab");

                        foreach (var direction in directions)
                        {
                            var rotatedDirection = rotatedDirections[i, (int)direction];
                            var neighborTargetSlotType = targetSlotType.neighbors[(int)rotatedDirection];

                            var connector1 = prototype.connectors[(int)direction];

                            var inverseDirection = inverseDirections[(int)direction];
                            foreach (var otherPrototype in target.prototypeCollection.prototypes)
                            {
                                var otherSlot = otherPrototype.slot;
                                for (int j = 0; j < otherSlot.targetSlotTypes.Length; j++)
                                {
                                    if (otherSlot.targetSlotTypes[j] == neighborTargetSlotType)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }

                AssetDatabase.SaveAssets();
            }
        }
    }
}
