using UnityEngine;

public class FoodScript : MonoBehaviour {
    public bool Frozen {get;set;}
    public bool Activated {get;set;}
    public float weight;
    public float life;
    public float freeze;
    public float lifeLoss;
    public float lifeLossTime;
    public float foodLifetime = 60;
    
    void Update() {
        if(Frozen) return;
        foodLifetime -= Time.deltaTime;
        if(foodLifetime < 0) Destroy(gameObject);       
    }
}