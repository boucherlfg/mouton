//#define GIBELOTTE

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    private List<Ingredient> ingredients = new();
    [SerializeField]
    private List<Recipe> recipes = new();
    [SerializeField]
    private Recipe defaultRecipe;

    private HandScript hand;
    public void Interact() {
        var choice = recipes.Find(CanDoRecipe);
        if(choice) {
            Craft(choice);
            return;
        }
        #if GIBELOTTE
        else if(ingredients.Count > 1) {
            Craft(defaultRecipe);
        }
        #endif
        else {
            DontCraft();
        }
        
    }

    private void DontCraft() {
        ingredients.ForEach(Drop);
    }
    
    private void Craft(Recipe recipe) {
        ingredients.ForEach(ingredient => {
            Destroy(ingredient.gameObject);
        });
        ingredients.Clear();

        foreach(var output in recipe.outputs) {
            var result = Instantiate(output.gameObject, transform.position, Quaternion.identity);
            result.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3 + Random.insideUnitCircle;
        }
    }

    bool CanDoRecipe(Recipe recipe) {
        foreach(var input in recipe.inputs) {
            var inputCount = recipe.inputs.Count(x => x.type == input.type);
            var ingredientCount = ingredients.Count(x => x.type == input.type);
            if(inputCount > ingredientCount) return false;
        }
        return true;
    }

    void Update() {
        hand = hand ? hand : FindObjectOfType<HandScript>();
        if(hand.carried && hand.carried.TryGetComponent(out Ingredient ingredient)) {
            var choice = ingredients.Find(x => x == ingredient);
            if(choice) Drop(ingredient);
        }
        var targets = new Vector2[] { 
            (Vector2)transform.position + Vector2.up, 
            (Vector2)transform.position + Vector2.up + Vector2.left * 0.5f, 
            (Vector2)transform.position + Vector2.up + Vector2.right * 0.5f 
        };
        
        for(int i = 0; i < ingredients.Count; i++) {
            var velocity = targets[i] - (Vector2)ingredients[i].transform.position;
            if(velocity.magnitude < 0.1f) velocity = Vector2.zero;
            else velocity = velocity.normalized * 5;
            ingredients[i].GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }

    void Add(Ingredient ingredient) {
        
        ingredient.GetComponent<FoodScript>().Frozen = true;
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 0;
        ingredients.Add(ingredient);
    }

    void Drop(Ingredient ingredient) {
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 1;
        ingredient.GetComponent<ItemInteractivity>().Activated = false;
        ingredient.GetComponent<FoodScript>().Frozen = false;
        ingredients.Remove(ingredient);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(ingredients.Count >= 3) return;
        if(!other.TryGetComponent(out Ingredient ingredient)) return;
        if(!ingredient.Activated) return;
        if(ingredients.Contains(ingredient)) return;
        Add(ingredient);
    }
}
