using UnityEngine;

public class FoodScript : MonoBehaviour {
    public bool Frozen {get;set;}
    public bool Activated {get;set;}
    public float weight;
    public float life;
    public float freeze;
    public float lifeLoss;
    public float lifeLossTime;
    public float foodLifetime = 30;
    private float foodCounter = 0;
    void Update() {
        if(Frozen) {
            foodCounter = 0;
            return;
        }
        foodCounter += Time.deltaTime;
        if(foodCounter > foodLifetime) Destroy(gameObject);       
    }
}