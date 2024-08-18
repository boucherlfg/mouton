using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(!other.GetComponent<ItemInteractivity>().Activated) return;
        Destroy(other.gameObject);
    }
}
