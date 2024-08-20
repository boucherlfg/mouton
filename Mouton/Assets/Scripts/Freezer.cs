using UnityEngine;

public class Freezer : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent(out KillAfterTime food)) return;
        food.Frozen = true;
        food.frozenFromFreezer = true;
    }
    public void OnTriggerExit2D(Collider2D other) {
        if(!other.TryGetComponent(out KillAfterTime food)) return;
        food.Frozen = false;
        food.frozenFromFreezer = false;
    }
}