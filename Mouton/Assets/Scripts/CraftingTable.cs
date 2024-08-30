//#define GIBELOTTE

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject ingredientPrefab;
    public AudioClip goodRecipe;
    public AudioClip badRecipe;
    public AudioClip placeItem;
    public AudioClip done;

    private Animator animator;
    private List<IngredientScript> ingredients = new();
    public List<Recipe> recipes = new();
#if GIBELOTTE
    [SerializeField]
    private Recipe defaultRecipe;
#endif
    public bool Working {get; private set;}
    private HandScript hand;
    public void Interact() 
    {
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
        
        GameObject result;
        if(recipe.output is Platform platform) {
            result = Instantiate(platform.prefab, transform.position, Quaternion.identity);
        }
        else {
            var prefab = recipe.output is Food ? foodPrefab : ingredientPrefab;
            result = Instantiate(prefab, transform.position, Quaternion.identity);
            result.GetComponent<IngredientScript>().ingredient = recipe.output;
        }
        result.GetComponent<Rigidbody2D>().velocity = Vector2.up * 3 + Random.insideUnitCircle;
    }

    bool CanDoRecipe(Recipe recipe) {
        var recipeDict = new Dictionary<Ingredient, int>();
        recipe.inputs.ToList().ForEach(x => recipeDict[x] = recipeDict.ContainsKey(x) ? recipeDict[x] + 1 : 1);

        var ingredientsDict = new Dictionary<Ingredient, int>();
        ingredients.ToList().ForEach(x => ingredientsDict[x.ingredient] = ingredientsDict.ContainsKey(x.ingredient) ? ingredientsDict[x.ingredient] + 1 : 1);

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
        if(hand.carried && hand.carried.TryGetComponent(out IngredientScript ingredient)) {
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

    void Add(IngredientScript ingredient) {
        ingredient.GetComponent<KillAfterTime>().Frozen = true;
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 0;
        ingredients.Add(ingredient);
    }

    void Drop(IngredientScript ingredient) {
        ingredient.GetComponent<Rigidbody2D>().gravityScale = 1;
        ingredient.GetComponent<ItemInteractivity>().Activated = false;
        ingredient.GetComponent<KillAfterTime>().Frozen = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(Working) return;
        if(ingredients.Count >= 3) return;
        if(!other.TryGetComponent(out IngredientScript ingredient)) return;
        if(!ingredient.Activated) return;
        if(ingredients.Contains(ingredient)) return;

        AudioSource.PlayClipAtPoint(placeItem, transform.position);
        Add(ingredient);
    }
}
