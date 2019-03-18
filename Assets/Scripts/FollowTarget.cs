using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}
