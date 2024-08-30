using System.Collections.Generic;
using UnityEngine;
public class TemporaryLifeLoss {
        public float lifeLoss;
        public float lifeLossTime;
        public TemporaryLifeLoss(float lifeLoss, float lifeLossTime) {
            this.lifeLoss = lifeLoss;
            this.lifeLossTime = lifeLossTime;
        }
    }
public class SheepScript : MonoBehaviour
{   
    public AudioClip sheepFood;
    public AudioClip sheepDead;
    [Range(1, 5)]
    public float slopeFactor = 1;
    [Range(0, 3)]
    public float scaleSpeed = 0.4f;
    public float weight = 0;
    public float baseLife = 120;
    [HideInInspector]
    public float currentLife;
    [SerializeField]
    private float baseLifeLoss = 1;
    public float freeze = 60;
    private List<TemporaryLifeLoss> lifeLosses = new();
    private bool isDead;
    [SerializeField]
    private Transform body;
 
    void OnTriggerEnter2D(Collider2D other) {
        if(isDead) return;
        if(!other.TryGetComponent(out FoodScript food)) return;
        if(!food.Activated) return;
        Eat(food);
    }

    void Start() {
        currentLife = baseLife;
    }

    void Update() {


        var scale = 1 + Mathf.Pow(scaleSpeed * weight, 1/slopeFactor);
        float size = body.localScale.x;
        float delta = scale - size;
        if(Mathf.Abs(delta) > Time.deltaTime) size += Mathf.Sign(delta) * Time.deltaTime;
        body.localScale = size * Vector3.one;
        
        if(isDead) return;

        freeze -= Time.deltaTime;
        freeze = Mathf.Max(0, freeze);
        if(freeze > 0.01f)  return;

        lifeLosses.ForEach(l => currentLife -= l.lifeLoss * Time.deltaTime);
        lifeLosses.ForEach(l => l.lifeLossTime -= Time.deltaTime);
        lifeLosses.RemoveAll(l => l.lifeLossTime <= 0);

        currentLife -= baseLifeLoss * Time.deltaTime;

        if(currentLife < 0) Die();
    }
    
    public void Die() {
        isDead = true;
        AudioSource.PlayClipAtPoint(sheepDead, transform.position);
        ServiceManager.Instance.Get<OnGameEnded>().Invoke();
    }
   
   void Eat(FoodScript foodScript) {
        var ingredient = foodScript.GetComponent<IngredientScript>().ingredient;
        if(ingredient is not Food) return;
        
        var food = ingredient as Food;
        AudioSource.PlayClipAtPoint(sheepFood, transform.position);
        freeze += food.freeze;
        currentLife += food.life;
        weight += food.weight;
        lifeLosses.Add(new TemporaryLifeLoss(food.lifeLoss, food.lifeLossTime));
        ServiceManager.Instance.Get<OnSheepEat>().Invoke(food);
        Destroy(foodScript.gameObject);
    }
}
