using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private Animator animator;
    public float unhealthyPercent = 0.25f;
    private SheepScript sheep;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sheep = FindObjectOfType<SheepScript>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentLife = sheep.currentLife;
        var baseLife = sheep.baseLife;
        var scale = Mathf.Min(currentLife / baseLife, 1);
        GetComponent<RectTransform>().localScale = scale * Vector3.one;

        if(sheep.freeze > 0) animator.Play("Frozen");
        else if(scale < unhealthyPercent) animator.Play("Unhealthy");
        else animator.Play("Healthy");

        if(scale < 0) Destroy(gameObject);
    }
}
