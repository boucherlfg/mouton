using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {
    [Scene]
    public string endScene;
    void Start() {
        ServiceManager.Instance.Get<OnGameEnded>().Subscribe(HandleGameEnded);
    }
    void OnDestroy() {
        ServiceManager.Instance.Get<OnGameEnded>().Unsubscribe(HandleGameEnded);
    }
    void HandleGameEnded() {
        var sheep = FindObjectOfType<SheepScript>();
        PlayerPrefs.SetFloat("weight", sheep.weight);
        PlayerPrefs.SetFloat("life", sheep.life);
        SceneManager.LoadScene(endScene);
    }
}