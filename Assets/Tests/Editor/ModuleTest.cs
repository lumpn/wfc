//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using NUnit.Framework;
using UnityEditor;

namespace Lumpn.WFC.Tests
{
    [TestFixture]
    public sealed class ModuleTest
    {
        [Test]
        public void AllowedNeighbors()
        {
            var guids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Prefabs/Modules" });
            foreach(var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var module = AssetDatabase.LoadAssetAtPath<Module>(path);
                foreach (var allowed in module.allowed)
                {
                    Assert.NotZero(allowed, module.name);
                }
            }
        }
    }
}
