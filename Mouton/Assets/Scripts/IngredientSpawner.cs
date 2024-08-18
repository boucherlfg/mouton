using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public List<GameObject> ingredients;
    public float spawnRate;
    private float counter;

    void Start() => counter = spawnRate;
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter < spawnRate) return;
        counter = 0;

        var item = ingredients.GetRandom();
        var position = new Vector2(Random.Range(transform.position.x, transform.position.x + transform.localScale.x), 
                                   Random.Range(transform.position.y, transform.position.y + transform.localScale.y));
        Instantiate(item, position, Quaternion.identity);
    }

    void OnDrawGizmosSelected() {
        #if UNITY_EDITOR
        UnityEditor.Handles.DrawSolidRectangleWithOutline(new Rect(transform.position, transform.localScale), Vector4.zero, Color.red);
        #endif
    }
}
