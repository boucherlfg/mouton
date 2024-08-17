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

    public void Interact() {
        var choice = recipes.Find(CanDoRecipe);
        if(choice) {
            Craft(choice);
            return;
        }
        else if(ingredients.Count <= 1) {
            DontCraft();
        }
        else {
            Craft(defaultRecipe);
        }
        
    }

    private void DontCraft() {
        ingredients.ForEach(ingredient => {
            ingredient.GetComponent<Rigidbody2D>().gravityScale = 1;
            foreach(var collider in ingredient.GetComponents<Collider2D>()) {
                collider.enabled = true;
                collider.GetComponent<ItemInteractivity>().Activated = false;
                collider.GetComponent<FoodScript>().Frozen = false;
            }
        });
        ingredients.Clear();
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

    void OnTriggerEnter2D(Collider2D other) {
        if(ingredients.Count >= 3) return;
        if(!other.TryGetComponent(out Ingredient ingredient)) return;
        if(!ingredient.Activated) return;
        if(ingredients.Contains(ingredient)) return;

        ingredient.GetComponent<FoodScript>().Frozen = true;
        ingredients.Add(ingredient);
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 0;
        foreach(var collider in ingredient.GetComponents<Collider2D>()) collider.enabled = false;
    }
}
