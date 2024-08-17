using UnityEngine;

public class EndGameMenu : MonoBehaviour {
    public TMPro.TMP_Text weight;
    public TMPro.TMP_Text life;

    public void Start() {
        var sheep = FindObjectOfType<SheepScript>();
        var weight = sheep.weight;
        var life = sheep.life;
        var currentLife = sheep.currentLife;

        this.weight.text = "your sheep weights " + Mathf.Round(weight) + " kg";
        this.life.text = "its health at the end was " + ((float)((int)(100 * currentLife / life) * 100))/100 + " %";
    }
}