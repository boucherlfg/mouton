using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpHeight = 20;
    void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot) 
            && !collision.gameObject.GetComponent<IngredientScript>() 
            && !collision.gameObject.GetComponent<FoodScript>()) return;

        Debug.Log("collision works");
        var rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if(!rigidBody) rigidBody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * rigidBody.velocity.x + Vector2.up * jumpHeight;
        GetComponent<Animator>().Play("Trampoline");
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.TryGetComponent(out FootScript foot) 
            && !collision.gameObject.GetComponent<IngredientScript>() 
            && !collision.gameObject.GetComponent<FoodScript>()) return;

        Debug.Log("trigger works");
        var rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if(!rigidBody) rigidBody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * rigidBody.velocity.x + Vector2.up * jumpHeight;

        GetComponent<Animator>().Play("Trampoline");
    }
}
