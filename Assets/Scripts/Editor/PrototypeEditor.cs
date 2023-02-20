//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.WFC
{
    [CustomEditor(typeof(Prototype))]
    public sealed class PrototypeEditor : Editor<Prototype>
    {
        protected override void OnInspectorGUI(Prototype target)
        {
            if (GUILayout.Button("Convert to interior"))
            {
                ReplaceConnectors(target, "Exterior", "Interior");
                EditorUtility.SetDirty(target);
            }
            if (GUILayout.Button("Convert to exterior"))
            {
                ReplaceConnectors(target, "Interior", "Exterior");
                EditorUtility.SetDirty(target);
            }
        }

        private static void ReplaceConnectors(Prototype prototype, string a, string b)
        {
            for (int i = 0; i < prototype.connectors.Length; i++)
            {
                var connector = prototype.connectors[i];
                if (connector.name.Contains(a, System.StringComparison.OrdinalIgnoreCase))
                {
                    var path = AssetDatabase.GetAssetPath(connector);
                    var newPath = path.Replace(a, b, System.StringComparison.OrdinalIgnoreCase);

                    var newConnector = AssetDatabase.LoadAssetAtPath<Connector>(newPath);
                    if (newConnector)
                    {
                        prototype.connectors[i] = newConnector;
                    }
                    else
                    {
                        Debug.LogErrorFormat(connector, "Failed to load replacement for '{0}' at '{1}'", connector.name, newPath);
                    }
                }
            }
        }
    }
}
