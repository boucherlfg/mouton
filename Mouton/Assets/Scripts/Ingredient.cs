using System.Collections;
using UnityEngine;

public enum IngredientType {
    Wheat = 0,
    Tomato = 1,
    Cheese = 2,
    Chicken = 3,
    Lettuce = 4,
    Bread = 5,
    Spaghetti = 6,
    TomatoSalad = 7,
    CheeseSlice = 8,
    FriedChicken = 9,
    GrilledChicken = 10,
    Bolognaise = 11,
    ChickenSalad = 12,
    CheeseBurger = 13,
    TomatoSauce = 14,
    Wool = 15,
    Stem = 16,
    GratedCheese = 17,
}
public class Ingredient : MonoBehaviour {
    public IngredientType type;
    public float foodLifetime = 30;
    public bool Frozen {get;set;}
    private float foodCounter = 0;
    public bool Activated {get;set;}
    public GameObject ice;
    public bool frozenFromFreezer;
    
    void Start() {
        StartCoroutine(Flicker());
    }
    void Update() {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.ClampMagnitude(body.velocity, 8);
        
        
        ice.SetActive(frozenFromFreezer);
        if(Frozen) {
            foodCounter = 0;
            return;
        }
        foodCounter += Time.deltaTime;
        if(foodCounter > foodLifetime) Destroy(gameObject);       
    }

    IEnumerator Flicker() {
        var rend = GetComponent<SpriteRenderer>();
                
        while(true) {
            if(foodCounter / foodLifetime > 0.75f) {
                var color = rend.color;
                color.a = 0.5f;
                rend.color = color;
                yield return new WaitForSeconds(0.25f);
                color.a = 1f;
                rend.color = color;
                yield return new WaitForSeconds(0.25f);
            }
            else {
                var color = rend.color;
                color.a = 1;
                rend.color = color;
                yield return null;
            }
        }
    }
}