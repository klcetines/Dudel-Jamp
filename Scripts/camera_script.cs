using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.5f;  // Adjust for smoothness
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(0, player.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
