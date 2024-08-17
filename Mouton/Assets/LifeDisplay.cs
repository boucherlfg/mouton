using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public Color frozenColor;
    public Color regularColor;
    private SheepScript sheep;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        sheep = FindObjectOfType<SheepScript>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = sheep.currentLife / sheep.baseLife;
        slider.fillRect.GetComponent<Image>().color = sheep.freeze > 0.1f ? frozenColor : regularColor;
    }
}
