using UnityEngine;

public static class GizmosExtension
{

    public static void DrawArrow(Vector3 from, Vector3 to, float headSize = 1f)
    {
        Gizmos.DrawLine(from, to);

        DrawArrowHead(from, to, headSize);
    }

    public static void DrawArrowHead(Vector3 from, Vector3 to, float headSize = 1f)
    {
        var aux = (from - to).normalized;

        Gizmos.DrawLine(to, to + Quaternion.Euler(0, 20, 0) * aux);
        Gizmos.DrawLine(to, to + Quaternion.Euler(0, -20, 0) * aux);
    }

    public static void DrawPoint(Vector3 position, float size = 1f)
    {
        Gizmos.DrawLine(position + Vector3.right * 0.5f * size, position - Vector3.right * 0.5f * size);
        Gizmos.DrawLine(position + Vector3.up * 0.5f * size, position - Vector3.up * 0.5f * size);
        Gizmos.DrawLine(position + Vector3.forward * 0.5f * size, position - Vector3.forward * 0.5f * size);
    }
}
