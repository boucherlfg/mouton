using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAnimation : MonoBehaviour
{
    private OnSheepEat sheepEat;
    private OnGameEnded gameEnd;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sheepEat = ServiceManager.Instance.Get<OnSheepEat>();
        gameEnd = ServiceManager.Instance.Get<OnGameEnded>();

        sheepEat.Subscribe(HandleSheepEat);
        gameEnd.Subscribe(HandleGameEnded);
    }
    void OnDestroy() {
        sheepEat.Unsubscribe(HandleSheepEat);
        gameEnd.Unsubscribe(HandleGameEnded);
    }

    private void HandleGameEnded()
    {
        animator.Play("Dead");
    }

    private void HandleSheepEat(FoodScript script)
    {
        animator.Play("Eat");
    }
}
