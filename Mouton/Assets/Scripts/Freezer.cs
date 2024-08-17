using UnityEngine;

public class Freezer : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent(out FoodScript food)) return;
        food.Frozen = true;
    }
    public void OnTriggerExit2D(Collider2D other) {
        if(!other.TryGetComponent(out FoodScript food)) return;
        food.Frozen = false;
    }
}