using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ingredientPrefab;
    [SerializeField]
    private GameObject foodPrefab;

    public float ingredientSpawnDelay = 3;
    public List<Ingredient> ingredients;
    public float spawnRate;
    private float counter;

    void Start() => counter = spawnRate;
    // Update is called once per frame
    void Update()
    {
        ingredientSpawnDelay -= Time.deltaTime;
        if(ingredientSpawnDelay > 0) return;
        
        counter += Time.deltaTime;
        if(counter < spawnRate) return;
        counter = 0;

        var item = ingredients.GetRandom();
        var position = new Vector2(Random.Range(transform.position.x, transform.position.x + transform.localScale.x), 
                                   Random.Range(transform.position.y, transform.position.y + transform.localScale.y));
        var instance = Instantiate(item is Food ? foodPrefab : ingredientPrefab, position, Quaternion.identity);
        instance.GetComponent<IngredientScript>().ingredient = item;
    }

    void OnDrawGizmosSelected() {
        #if UNITY_EDITOR
        UnityEditor.Handles.DrawSolidRectangleWithOutline(new Rect(transform.position, transform.localScale), Vector4.zero, Color.red);
        #endif
    }
}
