using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect : MonoBehaviour
{
    public Collider2D platform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        var move = ServiceManager.Instance.Get<InputService>().Move;
        platform.enabled = move.y > -0.5f;
    }
    
}
