using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 10;

    private Rigidbody2D _rigidbody2D;
    private InputService inputService;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputService = ServiceManager.Instance.Get<InputService>();
        inputService.Jumped += HandleJump;
        FootScript.TouchedGround += HandleTouchedGround;
    }

    void OnDestroy() {
        inputService.Jumped -= HandleJump;
        FootScript.TouchedGround -= HandleTouchedGround;
    }

    void HandleTouchedGround() {
        isJumping = false;
    }

    void HandleJump() {
        if(isJumping) return;
        isJumping = true;
        _rigidbody2D.velocity = Vector2.right * _rigidbody2D.velocity.x + jumpHeight * Vector2.up;
    }
}
