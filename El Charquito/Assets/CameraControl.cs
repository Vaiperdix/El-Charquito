using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 4f;
    public float yOffset = 1f;
    Transform target;

    private void Start()
    {
        target = PlayerController.Instance.transform;
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed);
    }
}
