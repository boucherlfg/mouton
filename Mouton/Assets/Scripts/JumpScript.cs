using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 10;
    public float gravityScale = 3;
    public AudioClip jumpSound;
    public AudioClip landSound;
    private Rigidbody2D _rigidbody2D;
    private InputService inputService;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputService = ServiceManager.Instance.Get<InputService>();
        inputService.Jumped += HandleJump;
        ServiceManager.Instance.Get<OnTouchedGround>().Subscribe(HandleTouchedGround);
        _rigidbody2D.gravityScale = gravityScale;
    }

    void OnDestroy() {
        inputService.Jumped -= HandleJump;
        ServiceManager.Instance.Get<OnTouchedGround>().Unsubscribe(HandleTouchedGround);
    }

    void HandleTouchedGround() {
        isJumping = false;
        AudioSource.PlayClipAtPoint(landSound, transform.position);
    }

    void HandleJump() {
        if(isJumping) return;
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        isJumping = true;
        _rigidbody2D.velocity = Vector2.right * _rigidbody2D.velocity.x + jumpHeight * Vector2.up;
    }
}
