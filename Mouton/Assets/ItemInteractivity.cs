using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractivity : MonoBehaviour
{
    public bool Activated {
        get;set;
    } = false;

    // Update is called once per frame
    void Update()
    {
        if(TryGetComponent(out FoodScript food)) food.Activated = Activated;
        if(TryGetComponent(out Ingredient ingredient)) ingredient.Activated = Activated;
    }
}
