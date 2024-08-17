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
        GetComponent<FoodScript>().Activated = Activated;
        GetComponent<Ingredient>().Activated = Activated;
    }
}
