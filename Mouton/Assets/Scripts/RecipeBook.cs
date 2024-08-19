using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    public CraftingTable table;
    public CraftingTable chauldron;
    public GameObject recipePrefab;
    public Transform container;
    public GameObject header;

    // Start is called before the first frame update
    void Start()
    {
        var instance = Instantiate(header, container);
        instance.GetComponentInChildren<Image>().sprite = table.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        instance.GetComponentInChildren<TMPro.TMP_Text>().text = "Table";

        foreach(var recipe in table.recipes) {
            instance = Instantiate(recipePrefab, container);
            instance.GetComponent<RecipeMenuItem>().UpdateRecipe(recipe);
        }

        instance = Instantiate(header, container);
        instance.GetComponentInChildren<Image>().sprite = chauldron.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        instance.GetComponentInChildren<TMPro.TMP_Text>().text = "Chauldron";
        
        foreach(var recipe in chauldron.recipes) {
            instance = Instantiate(recipePrefab, container);
            instance.GetComponent<RecipeMenuItem>().UpdateRecipe(recipe);
        }
    }

}
