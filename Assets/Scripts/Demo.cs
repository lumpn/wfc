using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Demo : MonoBehaviour
{
    [System.Serializable]
    public struct Tile3
    {
        public Mesh mesh;
        public int f0, f1, f2;
    }

    [SerializeField] private Mesh triangulation;
    [SerializeField] private Tile3[] tiles;
    [SerializeField] private Material material;
    [SerializeField] private int seed;
    [SerializeField, Range(0, 1)] private float probability;

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

        var builtVerts = new List<Vector3>();
        var builtTris = new List<int>();

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

            var tileMesh = FindTile(tiles, t0, t1, t2, out Vector3 p0, out Vector3 p1, out Vector3 p2);
            if (!tileMesh) continue;

            var sourceMatrix = CreateMatrix(p0, p1, p2);

            //var inverseSourceMatrix = sourceMatrix.inverse;
            var inverseSourceMatrix = new Matrix4x4();
            bool success = Matrix4x4.Inverse3DAffine(sourceMatrix, ref inverseSourceMatrix);
            if (!success)
            {
                Debug.LogErrorFormat("Failed to invert matrix for triangle at {0}\n{1}", i, sourceMatrix);
                continue;
            }
            //else
            //{
            //    Debug.LogFormat("Source\n{0}\nInverse\n{1}", sourceMatrix, inverseSourceMatrix);
            //}

            var targetMatrix = CreateMatrix(v0, v1, v2);
            var transformation = targetMatrix * inverseSourceMatrix;

            var meshVerts = tileMesh.vertices;
            var meshTris = tileMesh.triangles;

            var vertOffset = builtVerts.Count;
            for (int j = 0; j < meshVerts.Length; j++)
            {
                var v = meshVerts[j];
                var xz = new Vector3(v.x, v.z, 1);
                var w = transformation.MultiplyPoint3x4(xz);
                w.z = w.y;
                w.y = v.y;
                builtVerts.Add(w);
            }

            for (int j = 0; j < meshTris.Length; j++)
            {
                var t = meshTris[j];
                builtTris.Add(vertOffset + t);
            }
        }

        var mesh = new Mesh();
        mesh.SetVertices(builtVerts);
        mesh.SetTriangles(builtTris, 0);
        mesh.Optimize();

        var go = new GameObject();
        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();
        mf.sharedMesh = mesh;
        mr.sharedMaterial = material;
        go.transform.SetParent(transform, false);
    }

    private static Matrix4x4 CreateMatrix(Vector3 a, Vector3 b, Vector3 c)
    {
        var p = ToVector4(a);
        var q = ToVector4(b);
        var r = ToVector4(c);
        var s = Vector4.zero;
        return new Matrix4x4(p, q, r, s);
    }

    private static Vector4 ToVector4(Vector3 v)
    {
        return new Vector4(v.x, v.z, 1, 0);
    }

    private static Mesh FindTile(Tile3[] tiles, int t0, int t1, int t2, out Vector3 p0, out Vector3 p1, out Vector3 p2)
    {
        for (int idx = 0; idx < tiles.Length; idx++)
        {
            var tile = tiles[idx];
            var f0 = tile.f0;
            var f1 = tile.f1;
            var f2 = tile.f2;

            p0 = new Vector3(0, 0, 0);
            p1 = new Vector3(1, 0, -2);
            p2 = new Vector3(-1, 0, -2);

            for (int rotation = 0; rotation < 3; rotation++)
            {
                if (HasFlag(t0, f0) && HasFlag(t1, f1) && HasFlag(t2, f2))
                {
                    return tile.mesh;
                }

                var tmp = f0;
                f0 = f1;
                f1 = f2;
                f2 = tmp;

                var ptmp = p0;
                p0 = p1;
                p1 = p2;
                p2 = ptmp;
            }
        }

        p0 = Vector3.zero;
        p1 = Vector3.zero;
        p2 = Vector3.zero;
        return null;
    }

    private static bool HasFlag(int flag, int mask)
    {
        return (flag & mask) == flag;
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
        if (type < 2) return;

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var tr = cube.transform;
        tr.localPosition = pos;
        tr.localScale = Vector3.one / 2;
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
            var type = (random.NextDouble() <= probability) ? 2 : 1;
            result[i] = type;
        }

        return result;
    }

    private static int[] CreateTypes2(int num, int seed, float probability)
    {
        var result = new int[num];
        for (int i = 0; i < num; i++)
        {
            var type = (i < seed) ? 2 : 1;
            result[i] = type;
        }

        return result;
    }
}
