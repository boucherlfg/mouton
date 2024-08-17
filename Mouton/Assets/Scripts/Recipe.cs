using UnityEngine;

[CreateAssetMenu(menuName = "Mouton/Recette")]
public class Recipe : ScriptableObject {
    public float prepTime;
    public Ingredient[] inputs;
    public GameObject[] outputs;
}