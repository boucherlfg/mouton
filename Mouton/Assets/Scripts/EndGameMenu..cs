using UnityEngine;

public class EndGameMenu : MonoBehaviour {
    public TMPro.TMP_Text weight;
    public TMPro.TMP_Text life;

    public void Start() {
        var weight = PlayerPrefs.GetFloat("weight");
        var life = PlayerPrefs.GetFloat("life");

        this.weight.text = "your sheep weights " + Mathf.Round(weight) + " kg";
        this.life.text = "its health at the end was " + ((float)((int)(life / 120f) * 10000))/100 + " %";
    }
}