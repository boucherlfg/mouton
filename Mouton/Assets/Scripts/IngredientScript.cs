using UnityEngine;

public class IngredientScript : MonoBehaviour {
    public Ingredient ingredient;
    public bool Activated {get;set;}
    
    void Update() {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.ClampMagnitude(body.velocity, 8); 
        if(!ingredient) return;  
        GetComponent<SpriteRenderer>().sprite = ingredient.sprite;
        var outline = GetComponentInChildren<OutlineScript>();
        if(outline) outline.GetComponent<SpriteRenderer>().sprite = ingredient.sprite;
    }
}