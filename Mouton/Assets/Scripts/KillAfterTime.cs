using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    public bool Frozen {get;set;}
    public float foodLifetime = 30;
    private float foodCounter = 0;
    public GameObject ice;
    public bool frozenFromFreezer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
        ice.SetActive(frozenFromFreezer);
        if(Frozen) {
            foodCounter = 0;
            return;
        }
        foodCounter += Time.deltaTime;
        if(foodCounter > foodLifetime) Destroy(gameObject);    
    }
    IEnumerator Flicker() {
        var rend = GetComponent<SpriteRenderer>();
                
        while(true) {
            if(foodCounter / foodLifetime > 0.75f) {
                var color = rend.color;
                color.a = 0.5f;
                rend.color = color;
                yield return new WaitForSeconds(0.25f);
                color.a = 1f;
                rend.color = color;
                yield return new WaitForSeconds(0.25f);
            }
            else {
                var color = rend.color;
                color.a = 1;
                rend.color = color;
                yield return null;
            }
        }
    }
}
