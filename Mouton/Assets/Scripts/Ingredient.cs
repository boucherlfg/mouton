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
    public bool Activated {get;set;}
    
    void Start() {
    }
    void Update() {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.ClampMagnitude(body.velocity, 8);   
    }

}