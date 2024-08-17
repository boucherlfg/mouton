using UnityEngine;

public enum IngredientType {
    Farine,
    Pain,
}
public class Ingredient : MonoBehaviour {
    public bool Activated {get;set;}
    public IngredientType type;
    void Update() {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.ClampMagnitude(body.velocity, 8);
    }
}