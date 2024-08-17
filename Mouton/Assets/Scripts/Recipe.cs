using UnityEngine;

[CreateAssetMenu(menuName = "Mouton/Recette")]
public class Recipe : ScriptableObject {
    public Ingredient[] inputs;
    public Ingredient[] outputs;
}