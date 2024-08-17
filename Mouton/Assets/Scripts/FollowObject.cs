using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public float speed = 3;
    public GameObject toFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, toFollow.transform.position, Time.deltaTime * speed);
    }
}
