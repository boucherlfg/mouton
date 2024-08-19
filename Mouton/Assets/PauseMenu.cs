using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject howToMenu;

    void Start() {
        ServiceManager.Instance.Get<InputService>().Paused += OnPause;
    }
    void OnDestroy() {
        
        ServiceManager.Instance.Get<InputService>().Paused -= OnPause;
    }

    public void OnPause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart() {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void Howto() {
        howToMenu.SetActive(true);
    }
}
