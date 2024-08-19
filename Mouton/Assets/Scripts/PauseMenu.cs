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
        ServiceManager.Instance.Get<InputService>().Activated = false;
    }
    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        ServiceManager.Instance.Get<InputService>().Activated = true;
    }

    public void Restart() {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void Howto() {
        howToMenu.SetActive(true);
    }
}
