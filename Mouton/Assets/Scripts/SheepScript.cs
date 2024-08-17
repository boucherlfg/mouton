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
    private float weight = 0;
    private float life = 120;
    private float baseLifeLoss = 1;
    private float freeze = 60;
    private List<TemporaryLifeLoss> lifeLosses = new();

    public Transform body;
    void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent(out FoodScript food)) return;
        if(!food.Activated) return;
        Eat(food);
    }

    void Update() {
        if(freeze > 0) freeze -= Time.deltaTime;
        var lifeLoss = lifeLosses.Aggregate(baseLifeLoss, (current, i) => current + i.lifeLoss);
        lifeLosses.ForEach(l => l.lifeLossTime -= Time.deltaTime);
        lifeLosses.RemoveAll(l => l.lifeLossTime <= 0);

        var log = Mathf.Log(weight + 1);
        body.localScale = (1 + log * log) * Vector3.one;
    }

    void Eat(FoodScript food) {
        Destroy(food.gameObject);
        body.localScale += Vector3.one;
        freeze += food.freeze;
        life += food.life;
        weight += food.weight;
        lifeLosses.Add(new TemporaryLifeLoss(food.lifeLoss, food.lifeLossTime));
    }
}
