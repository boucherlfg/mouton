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
    private MoveScript move;
    private JumpScript jump;
    void Start() {
        animator = GetComponent<Animator>();
        hand = FindObjectOfType<HandScript>();
        move = FindObjectOfType<MoveScript>();
        jump = move.GetComponent<JumpScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_rigidBody.velocity.x * _rigidBody.velocity.x > 0.1f) lastVelocityX = Mathf.Sign(_rigidBody.velocity.x);

        transform.localScale = Vector3.right * lastVelocityX + Vector3.up  + Vector3.forward;

        string action = "";
        
        if(jump.isJumping) action = "Jump" + (_rigidBody.velocity.y < 0 ? "Down" : "Up");
        else if(Mathf.Abs(_rigidBody.velocity.x) > 0.1f) action = "Walk";
        else action = "Idle";

        if(hand.carried) action += "Hold";

        if(move.YAxisMove) action = "Climb" + (_rigidBody.velocity.magnitude < 0.1f ? "Idle" : "");

        animator.Play(action);
    }
}
