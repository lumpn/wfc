using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Demo : MonoBehaviour
{
    public struct Tile3
    {
        public Mesh mesh;
        public int f0, f1, f2;
    }

    [SerializeField] private Mesh triangulation;
    [SerializeField] private Tile3[] tiles;
    [SerializeField] private int seed;
    [SerializeField] private float probability;

    void Start()
    {
        BuildMesh();
    }

    [ContextMenu("Build mesh")]
    void BuildMesh()
    {
        DestroyChildren();

        var vertices = triangulation.vertices;
        var triangles = triangulation.triangles;

        // weld vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            var p = vertices[i];
            for (int j = i + 1; j < vertices.Length; j++)
            {
                var q = vertices[j];
                var delta = q - p;
                if (delta.sqrMagnitude < 0.01f)
                {
                    Replace(triangles, j, i);
                }
            }
        }

        var types = CreateTypes(vertices.Length, seed, probability);

        for (int i = 0; i < vertices.Length; i++)
        {
            for (int j = 0; j < triangles.Length; j++)
            {
                if (triangles[j] == i)
                {
                    var v = vertices[i];
                    var t = types[i];
                    CreateSentinel(v, t);
                    break;
                }
            }
        }

        for (int i = 0; i < triangles.Length; i += 3)
        {
            var i0 = triangles[i + 0];
            var i1 = triangles[i + 1];
            var i2 = triangles[i + 2];

            var v0 = vertices[i0];
            var v1 = vertices[i1];
            var v2 = vertices[i2];
            var t0 = types[i0];
            var t1 = types[i1];
            var t2 = types[i2];

            // rotate w.l.o.g.
            while (true)
            {
                if (t0 <= t1 && t1 <= t2) break;

                var tmp = i0;
                i0 = i1;
                i1 = i2;
                i2 = tmp;
            }


        }
    }

    private static void Replace(int[] tris, int a, int b)
    {
        for (int i = 0; i < tris.Length; i++)
        {
            if (tris[i] == a)
            {
                tris[i] = b;
            }
        }
    }

    void CreateSentinel(Vector3 pos, int type)
    {
        if (type < 1) return;

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var tr = cube.transform;
        tr.localPosition = pos;
        tr.SetParent(transform, false);
    }

    void DestroyChildren()
    {
        int num = transform.childCount;
        for (int i = num - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            DoDestroy(child.gameObject);
        }
    }

    private static void DoDestroy(GameObject go)
    {
        if (Application.isPlaying)
        {
            Object.Destroy(go);
        }
        else
        {
            Object.DestroyImmediate(go);
        }
    }

    private static int[] CreateTypes(int num, int seed, float probability)
    {
        var random = new System.Random(seed);

        var result = new int[num];
        for (int i = 0; i < num; i++)
        {
            var type = (random.NextDouble() <= probability) ? 1 : 0;
            result[i] = type;
        }

        return result;
    }

    private static int[] CreateTypes2(int num, int seed, float probability)
    {
        var result = new int[num];
        for (int i = 0; i < num; i++)
        {
            var type = (i < seed) ? 1 : 0;
            result[i] = type;
        }

        return result;
    }
}
