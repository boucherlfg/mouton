using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpHeight = 20;
    void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot) 
            && !collision.gameObject.GetComponent<Ingredient>() 
            && !collision.gameObject.GetComponent<FoodScript>()) return;
        if((transform.position - collision.transform.position).y < 0.1f) return;

        Debug.Log("collision works");
        FindObjectOfType<JumpScript>().GetComponent<Rigidbody2D>().velocity += Vector2.up;
        GetComponent<Animator>().Play("Trampoline");
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot) 
            && !collision.gameObject.GetComponent<Ingredient>() 
            && !collision.gameObject.GetComponent<FoodScript>()) return;
        var delta = transform.position - collision.transform.position;
        if(delta.y > 0.1f) return;

        Debug.Log("trigger works");
        FindObjectOfType<JumpScript>().GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpHeight;
        GetComponent<Animator>().Play("Trampoline");
    }
}
