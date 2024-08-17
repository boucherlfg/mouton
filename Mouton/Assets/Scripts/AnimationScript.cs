using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _rigidBody;
    float lastVelocityX = 1;
    private Animator animator;
    private HandScript hand;
    void Start() {
        animator = GetComponent<Animator>();
        hand = FindObjectOfType<HandScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_rigidBody.velocity.x * _rigidBody.velocity.x > 0.1f) lastVelocityX = Mathf.Sign(_rigidBody.velocity.x);

        transform.localScale = Vector3.right * lastVelocityX + Vector3.up  + Vector3.forward;

        string action = "";
        if(hand.carried) action += "Hold";
        if(Mathf.Abs(_rigidBody.velocity.x) > 0.1f) action = "Walk" + action;
        else action = "Idle" + action;

        animator.Play(action);
    }
}
