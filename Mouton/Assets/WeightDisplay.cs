using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightDisplay : MonoBehaviour
{
    private SheepScript sheep;
    public TMPro.TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        sheep = FindObjectOfType<SheepScript>();
    }

    // Update is called once per frame
    void Update()
    {
        var weight = sheep.weight;
        label.text = "" + Mathf.Round(50 + weight) + " kg";
    }
}
