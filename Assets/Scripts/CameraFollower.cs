using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;
    public Vector3 Distance = new Vector3(0, 0, -5);

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Target.position - Distance;
        transform.GetChild(0).LookAt(Target);
    }
}
