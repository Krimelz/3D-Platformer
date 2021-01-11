using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0.01f, 10f)]
    public float smoothSpeed = 2f;

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.LerpUnclamped(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }
}
