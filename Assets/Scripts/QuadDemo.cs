using UnityEngine;
using Unity.Mathematics;

public class QuadDemo : MonoBehaviour
{
    [SerializeField] private Transform a, b, c, d;
    [SerializeField, Range(0, 1)] private float u, v;
    [SerializeField] private Transform mapped;

    void Update()
    {
        var pos = GetBilerp(u, v);
        mapped.localPosition = new Vector3(pos.x, 0, pos.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(a.localPosition, b.localPosition);
        Gizmos.DrawLine(b.localPosition, d.localPosition);
        Gizmos.DrawLine(d.localPosition, c.localPosition);
        Gizmos.DrawLine(c.localPosition, a.localPosition);

        for (int i = 0; i <= 10; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                var gu = i * 0.1f;
                var gv = j * 0.1f;
                var pos = GetBilerp2(gu, gv);
                DrawCross(new Vector3(pos.x, 0, pos.y));
            }
        }
    }

    private static void DrawCross(Vector3 pos)
    {
        var dx = new Vector3(0.01f, 0, 0);
        var dz = new Vector3(0, 0, 0.01f);
        Gizmos.DrawLine(pos - dx, pos + dx);
        Gizmos.DrawLine(pos - dz, pos + dz);
    }

    private Vector2 GetBilerp(float bu, float bv)
    {
        var p00 = GetPosition(a);
        var p01 = GetPosition(b);
        var p10 = GetPosition(c);
        var p11 = GetPosition(d);

        var pos = Bilerp(p00, p01, p10, p11, bu, bv);
        return pos;
    }

    private float2 GetBilerp2(float bu, float bv)
    {
        var p00 = GetPosition2(a);
        var p01 = GetPosition2(b);
        var p10 = GetPosition2(c);
        var p11 = GetPosition2(d);

        var quad = new float2x4(p00, p01, p10, p11);
        return Bilerp(quad, new float2(bu, bv));
    }

    private static Vector2 GetPosition(Transform tr)
    {
        var pos = tr.localPosition;
        return new Vector2(pos.x, pos.z);
    }

    private static float2 GetPosition2(Transform tr)
    {
        var pos = tr.localPosition;
        return new float2(pos.x, pos.z);
    }

    private static Vector2 Bilerp(Vector2 p00, Vector2 p01, Vector2 p10, Vector2 p11, float u, float v)
    {
        var p0 = Vector2.LerpUnclamped(p00, p01, u);
        var p1 = Vector2.LerpUnclamped(p10, p11, u);
        var p = Vector2.LerpUnclamped(p0, p1, v);
        return p;

        // r  =  1-u
        // s  =  1-v
        // p0 = (1-u) * p00 + u * p01
        // p1 = (1-u) * p10 + u * p11
        // p  = (1-v) * p0  + v * p1
        //    = (1-v) * ((1-u) * p00 + u * p01) + v * ((1-u) * p10 + u * p11)
        //    =     s * (r * p00 + u * p01) + v * (r * p10 + u * p11)
        //    =     rs*p00 + us*p01 + rv*p10 + uv*p11
        //    = (p00, p01, p10, p11) * (rs, us, rv, uv)T
    }

    private static float2 Bilerp(float2x4 quad, float2 p)
    {
        // u = p.x
        // v = p.y
        // r = q.x
        // s = q.y
        float2 q = 1 - p;
        float4 f = new float4(q.x * q.y, p.x * q.y, q.x * p.y, p.x * p.y);
        return math.mul(quad, f);
    }
}
