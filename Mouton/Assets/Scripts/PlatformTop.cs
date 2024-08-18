using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTop : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot)) return;
        if((transform.position - foot.transform.position).y < 0.1f) return;

        Debug.Log("collision works");
        FindObjectOfType<JumpScript>().isJumping = false;
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot)) return;
        if((transform.position - foot.transform.position).y < 0.1f) return;

        Debug.Log("trigger works");
        FindObjectOfType<JumpScript>().isJumping = false;
    }
}
