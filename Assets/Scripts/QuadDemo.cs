using UnityEngine;
using Unity.Mathematics;

public class QuadDemo : MonoBehaviour
{

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
