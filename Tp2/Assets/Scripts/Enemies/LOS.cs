using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{

    public float range = 10;
    public float angle = 90;

    public LayerMask visibles = ~0;

    public bool IsInSight(Transform target)
    {
        var positionDifference = target.position - transform.position;
        var distance = positionDifference.magnitude;
        if (distance > range) return false;

        var angleToTarget = Vector3.Angle(transform.forward, positionDifference);
        if (angleToTarget > angle / 2) return false;

        RaycastHit rayInfo;
        if(Physics.Raycast(transform.position, positionDifference, out rayInfo, range, visibles))
        {
            if (rayInfo.transform != target)
                return false;
        }

        return true;
    }

    void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(position, range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

    }
}
