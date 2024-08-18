using UnityEngine;

public class EndGameMenu : MonoBehaviour {
    public TMPro.TMP_Text totalWeight;
    public TMPro.TMP_Text spoiledMeat;
    public TMPro.TMP_Text finalScore;
    public TMPro.TMP_Text highScore;

    public void Start() {
        var sheep = FindObjectOfType<SheepScript>();
        var weight = 50 + sheep.weight;
        var life = sheep.baseLife;
        var currentLife = sheep.currentLife;

        var spoiledMeat = weight * (1 - currentLife / life);
        var score = weight * currentLife / life;
        var highScore = PlayerPrefs.GetFloat("highScore", 0);
        bool newHighscore = score > highScore;
        if(newHighscore) {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }

        this.totalWeight.text = "Sheep weight : " + Mathf.Round(weight) + " kg";
        this.spoiledMeat.text = "Spoiled meat : " + Mathf.Round(spoiledMeat) + " kg";
        this.finalScore.text = "Final score : " + Mathf.Round(score) + " kg" + (newHighscore ? " (New highscore!)" : "");
        this.highScore.text = "Highscore : " + Mathf.Round(highScore) + " kg";
    }
}