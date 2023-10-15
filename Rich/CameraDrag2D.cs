using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag2D : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public CameraFollow cameraFollow;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            cameraFollow.isFollowing = false;
            return;
        }

        if (!Input.GetMouseButton(1))
        {
            cameraFollow.isFollowing = true;
            return;
        }

        Vector3 pos = dragOrigin - Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
    }
}
