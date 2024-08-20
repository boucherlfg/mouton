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
        ServiceManager.Instance.Get<InputService>().Resumed += Resume;
    }
    void OnDestroy() {
        
        ServiceManager.Instance.Get<InputService>().Paused -= OnPause;
        ServiceManager.Instance.Get<InputService>().Resumed -= Resume;
    }

    public void OnPause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        ServiceManager.Instance.Get<InputService>().Activated = false;
    }
    public void Resume() {
        howToMenu.SetActive(false);
        pauseMenu.SetActive(false);
        
        Time.timeScale = 1;
        ServiceManager.Instance.Get<InputService>().Activated = true;
    }

    public void Restart() {
        SceneManager.LoadScene(gameObject.scene.name);
        Time.timeScale = 1;
        ServiceManager.Instance.Get<InputService>().Activated = true;
    }

    public void Howto() {
        howToMenu.SetActive(true);
    }
}
