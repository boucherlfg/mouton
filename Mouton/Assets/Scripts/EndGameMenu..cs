using UnityEngine;

public class EndGameMenu : MonoBehaviour {
    public TMPro.TMP_Text totalWeight;
    public TMPro.TMP_Text extraMeat;
    public TMPro.TMP_Text finalScore;
    public TMPro.TMP_Text highScore;

    public float killButtonPenality = 0;

    public void Start() {
        var sheep = FindObjectOfType<SheepScript>();
        var weight = 50 + sheep.weight;
        var baseLife = sheep.baseLife;
        var currentLife = sheep.currentLife;

        var lifeRatio = currentLife / baseLife;
        var isDead = lifeRatio < 0.0001f;
        var extraMeat = weight * (isDead ? killButtonPenality : lifeRatio);

        var score = weight + extraMeat;

        var highScore = PlayerPrefs.GetFloat("highScore", 0);
        bool newHighscore = score > highScore;
        if(newHighscore) {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }

        this.totalWeight.text = "Sheep weight : " + Mathf.Round(weight) + " kg";
        this.extraMeat.text = "Kill button bonus : " + Mathf.Round(extraMeat) + " kg";
        this.finalScore.text = "Final score : " + Mathf.Round(score) + " kg" + (newHighscore ? " (New highscore!)" : "");
        this.highScore.text = "Highscore : " + Mathf.Round(highScore) + " kg";
    }
}