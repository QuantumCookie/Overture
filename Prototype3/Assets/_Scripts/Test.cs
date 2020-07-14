using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //public Transform target;

    [Range(0, 180)]
    public float angle = 10;
    [Range(1, 20)]
    public float length = 5;

    private void OnDrawGizmos()
    {
        //Vector3 direction = (target.position - transform.position).normalized;
        //angle = Vector3.Angle(direction, Vector3.forward);

        Vector3 direction = Vector3.forward;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.left);
        direction = rotation * direction;
        direction.Normalize();

        Gizmos.DrawLine(transform.position, transform.position + direction * length);

        //Vector3 v0 = direction;
        //Vector3 v0s = (Mathf.Abs(v0.y) > 0.707) ? Vector3.right : Vector3.up;
        //Vector3 v1 = Vector3.Cross(v0, v0s).normalized;
        //Vector3 v2 = Vector3.Cross(v0, v1).normalized;

        Vector3 v0 = direction;
        Vector3 v1 = new Vector3(0, -v0.z, v0.y).normalized;
        Vector3 v2 = Vector3.Cross(v0, v1).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + v1 * length);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + v2 * length);
    }
}
