using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameMenuManager : MonoBehaviour
{
    public UnityEvent gameEndedEvent;
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnGameEnded>().Subscribe(HandleGameEnded);
    }

    private void HandleGameEnded()
    {
        gameEndedEvent.Invoke();
    }
}
