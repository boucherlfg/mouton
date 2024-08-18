using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private Vector3 startPosition;
    public bool freezeX = false;
    public bool freezeY = false;
    public bool freezeZ = false;
    public float speed = 3;
    public GameObject toFollow;
    public Vector2 offset;

    void Start() {
        startPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        var positionWithOffset = toFollow.transform.position + (Vector3)offset;
        transform.position = Vector2.Lerp(transform.position, positionWithOffset, Time.deltaTime * speed);
        transform.position = new Vector3(freezeX ? startPosition.x : transform.position.x, freezeY ? startPosition.y : transform.position.y, freezeZ ? startPosition.z : transform.position.z);
    }
}
