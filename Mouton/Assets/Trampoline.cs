using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpHeight = 20;
    void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot)) return;
        if((transform.position - foot.transform.position).y < 0.1f) return;

        Debug.Log("collision works");
        FindObjectOfType<JumpScript>().GetComponent<Rigidbody2D>().velocity += Vector2.up;
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot)) return;
        var delta = transform.position - foot.transform.position;
        if(delta.y > 0.1f) return;

        Debug.Log("trigger works");
        FindObjectOfType<JumpScript>().GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpHeight;
    }
}
