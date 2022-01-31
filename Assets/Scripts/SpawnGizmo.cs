using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGizmo : MonoBehaviour
{
    public Color color1 = Color.blue;
    public float gizmoRadius = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color1;
        Gizmos.DrawSphere(transform.position, gizmoRadius);
    }
}
