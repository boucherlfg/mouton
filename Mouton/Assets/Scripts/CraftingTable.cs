//#define GIBELOTTE

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public AudioClip goodRecipe;
    public AudioClip badRecipe;
    public AudioClip placeItem;
    public AudioClip done;

    private Animator animator;
    private List<Ingredient> ingredients = new();
    [SerializeField]
    private List<Recipe> recipes = new();
    [SerializeField]
    private Recipe defaultRecipe;
    public bool Working {get; private set;}
    private HandScript hand;
    public void Interact() {
        if(Working) return;
        var choice = recipes.Find(CanDoRecipe);
        if(choice) {
            StartCoroutine(Craft(choice));
            return;
        }
        #if GIBELOTTE
        else if(ingredients.Count > 1) {
            Craft(defaultRecipe);
        }
        #endif
        else {
            AudioSource.PlayClipAtPoint(badRecipe, transform.position);
            DontCraft();
        }
    }

    private void DontCraft() {
        ingredients.ForEach(Drop);
        ingredients.Clear();
    }
    
    private IEnumerator Craft(Recipe recipe) {
        Working = true;
        ingredients.ForEach(ingredient => {
            Destroy(ingredient.gameObject);
        });
        ingredients.Clear();

        AudioSource.PlayClipAtPoint(goodRecipe, transform.position);
        animator.Play("Brewing");
        yield return new WaitForSeconds(recipe.prepTime);
        animator.Play("Idle");
        AudioSource.PlayClipAtPoint(done, transform.position);

        Working = false;
        foreach(var output in recipe.outputs) {
            var result = Instantiate(output, transform.position, Quaternion.identity);
            result.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3 + Random.insideUnitCircle;
        }
    }

    bool CanDoRecipe(Recipe recipe) {
        var recipeDict = new Dictionary<IngredientType, int>();
        recipe.inputs.ToList().ForEach(x => recipeDict[x.type] = recipeDict.ContainsKey(x.type) ? recipeDict[x.type] + 1 : 1);

        var ingredientsDict = new Dictionary<IngredientType, int>();
        ingredients.ToList().ForEach(x => ingredientsDict[x.type] = ingredientsDict.ContainsKey(x.type) ? ingredientsDict[x.type] + 1 : 1);

        foreach(var key in recipeDict.Keys) {
            if(!ingredientsDict.ContainsKey(key) || ingredientsDict[key] != recipeDict[key]) return false;
        }

        return ingredientsDict.Count == recipeDict.Count;
    }

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        hand = hand ? hand : FindObjectOfType<HandScript>();
        if(hand.carried && hand.carried.TryGetComponent(out Ingredient ingredient)) {
            var choice = ingredients.Find(x => x == ingredient);
            if(choice) { Drop(ingredient); ingredients.Remove(ingredient); }
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
        ingredient.GetComponent<Ingredient>().Frozen = true;
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 0;
        ingredients.Add(ingredient);
    }

    void Drop(Ingredient ingredient) {
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 1;
        ingredient.GetComponent<ItemInteractivity>().Activated = false;
        ingredient.GetComponent<Ingredient>().Frozen = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(Working) return;
        if(ingredients.Count >= 3) return;
        if(!other.TryGetComponent(out Ingredient ingredient)) return;
        if(!ingredient.Activated) return;
        if(ingredients.Contains(ingredient)) return;

        AudioSource.PlayClipAtPoint(placeItem, transform.position);
        Add(ingredient);
    }
}
