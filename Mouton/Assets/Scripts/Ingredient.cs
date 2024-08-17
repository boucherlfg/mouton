using UnityEngine;

public enum IngredientType {
    Farine,
    Pain,
}
public class Ingredient : MonoBehaviour {
    public bool Activated {get;set;}
    public IngredientType type;
}