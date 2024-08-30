using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAtPosition : MonoBehaviour
{
    public Vector2 spawn;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<MoveScript>()) {
            other.transform.position = spawn;
        }
        else if(other.GetComponent<IngredientScript>()) {
            Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(spawn, 0.2f);
    }
}
