//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(ModuleGenerator))]
    public class ModuleGeneratorEditor : Editor<ModuleGenerator>
    {
        private static readonly Direction[,] rotatedDirections =
        {
            {Direction.North, Direction.South, Direction.East, Direction.West, Direction.Up, Direction.Down},
            {Direction.East, Direction.West, Direction.South, Direction.North, Direction.Up, Direction.Down},
            {Direction.South, Direction.North, Direction.West, Direction.East, Direction.Up, Direction.Down},
            {Direction.West, Direction.East, Direction.North, Direction.South, Direction.Up, Direction.Down},
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

                var modules = new List<Module>();
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
                        module.slotType = targetSlotType;
                        module.prototype = prototype;
                        module.connectors = Rotate(prototype.connectors, i);

                        var parent = go.transform;

                        var clone = Object.Instantiate(prototype, position, rotation, parent);
                        clone.name = prototype.name;
                        Object.DestroyImmediate(clone);

                        var prefab = PrefabUtility.SaveAsPrefabAsset(go, $"Assets/Prefabs/Modules/{prototype.name}{i}.prefab");
                        Object.DestroyImmediate(go);

                        var prefabModule = prefab.GetComponent<Module>();
                        targetSlotType.modules.Add(prefabModule);

                        modules.Add(prefabModule);
                    }
                }

                foreach (var slotType in target.slotTypes)
                {
                    var candidates = new BitSet();
                    foreach (var module in slotType.modules)
                    {
                        candidates.UnionWith(new BitSet(1UL << module.id));
                    }
                    slotType.candidates = candidates.value;
                }

                foreach (var module in modules)
                {
                    module.allowed = new ulong[DirectionUtils.directions.Length];

                    var slotType = module.slotType;
                    foreach (var direction in DirectionUtils.directions)
                    {
                        var allowed = new BitSet();
                        var connector1 = module.connectors[(int)direction];

                        var neighborSlotType = slotType.neighbors[(int)direction];
                        var inverseDirection = DirectionUtils.inverseDirections[(int)direction];

                        foreach (var neighborModule in neighborSlotType.modules)
                        {
                            var connector2 = neighborModule.connectors[(int)inverseDirection];

                            // TODO Jonas: disallowed module pairs
                            if (connector1 == connector2)
                            {
                                allowed.UnionWith(new BitSet(1UL << neighborModule.id));
                            }
                        }

                        module.allowed[(int)direction] = allowed.value;
                    }

                    EditorUtility.SetDirty(module);
                }

                AssetDatabase.SaveAssets();
            }
        }

        private static Connector[] Rotate(Connector[] connectors, int rotation)
        {
            var result = new Connector[DirectionUtils.directions.Length];
            foreach (var direction in DirectionUtils.directions)
            {
                var rotatedDirection = rotatedDirections[rotation, (int)direction];
                result[(int)rotatedDirection] = connectors[(int)direction];
            }
            return result;
        }
    }
}
