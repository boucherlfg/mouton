using System.Collections;
using UnityEngine;

public enum IngredientType {
    Ble = 0,
    Tomate = 1,
    Fromage = 2,
    Poulet = 3,
    Laitue = 4,
    Pain = 5,
    Spag = 6,
    CubeTomate = 7,
    BolSalade = 8,
    SaladeTomate = 9,
    TrancheFromage = 10,
    PouletPane = 11,
    TranchePoulet = 12,
    SpagBolognaise = 13,
    SaladePoulet = 14,
    CheeseBurger = 15,
    SauceTomate = 16,
    Laine = 17,
    Tige = 18,
    FromageRape = 19,
}
public class Ingredient : MonoBehaviour {
    
    public float foodLifetime = 30;
    public bool Frozen {get;set;}
    private float foodCounter = 0;
    public bool Activated {get;set;}
    public IngredientType type;
    
    void Start() {
        StartCoroutine(Flicker());
    }
    void Update() {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = Vector2.ClampMagnitude(body.velocity, 8);
        
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