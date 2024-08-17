using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepScript : MonoBehaviour
{
    public class TemporaryLifeLoss {
        public float lifeLoss;
        public float lifeLossTime;
        public TemporaryLifeLoss(float lifeLoss, float lifeLossTime) {
            this.lifeLoss = lifeLoss;
            this.lifeLossTime = lifeLossTime;
        }
    }
    public float weight = 0;
    public float life = 120;
    [HideInInspector]
    public float currentLife;
    private float baseLifeLoss = 1;
    private float freeze = 60;
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
        currentLife = life;
    }
    void Update() {
        if(isDead) return;
        if(freeze > 0) freeze -= Time.deltaTime;

        var lifeLoss = lifeLosses.Aggregate(baseLifeLoss, (current, i) => current + i.lifeLoss);
        lifeLosses.ForEach(l => l.lifeLossTime -= Time.deltaTime);
        lifeLosses.RemoveAll(l => l.lifeLossTime <= 0);

        var log = Mathf.Log(weight + 1);
        body.localScale = (1 + log * log) * Vector3.one;

        currentLife -= lifeLoss * Time.deltaTime;
        currentLife -= Time.deltaTime;

        if(currentLife < 0) Die();
    }
    
    public void Die() {
        isDead = true;
        ServiceManager.Instance.Get<OnGameEnded>().Invoke();
    }
   
   void Eat(FoodScript food) {
        body.localScale += Vector3.one;
        freeze += food.freeze;
        currentLife += food.life;
        weight += food.weight;
        lifeLosses.Add(new TemporaryLifeLoss(food.lifeLoss, food.lifeLossTime));
        ServiceManager.Instance.Get<OnSheepEat>().Invoke(food);
        Destroy(food.gameObject);
    }
}
