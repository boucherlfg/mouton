using UnityEngine;
using UnityEngine.UI;

public class RecipeMenuItem : MonoBehaviour
{
    [SerializeField]
    private GameObject ingredient;
    [SerializeField]
    private GameObject plusSign;
    [SerializeField]
    private Image result;
    [SerializeField]
    private Transform ingredientContainer;

    public void UpdateRecipe(Recipe recipe) {
        result.GetComponent<Image>().sprite = recipe.output.sprite;

        // add ingredients
        for(int i = 0; i < recipe.inputs.Length; i++) {
            var input = recipe.inputs[i];
            var instance = Instantiate(ingredient, ingredientContainer);
            instance.SetActive(true);
            instance.GetComponent<Image>().sprite = input.sprite;
            if(i >= recipe.inputs.Length - 1) break;

            Instantiate(plusSign, ingredientContainer).SetActive(true);
        }
    }
}
