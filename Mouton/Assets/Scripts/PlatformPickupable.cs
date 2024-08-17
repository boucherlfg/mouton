using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPickupable : MonoBehaviour
{
    private InputService _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = ServiceManager.Instance.Get<InputService>();
        HandScript.Activated = false;
        GetComponent<Collider2D>().enabled = false;
        _input.LeftDown += HandleLeftDown;
    }

    void HandleLeftDown() {
        HandScript.Activated = true;
        GetComponent<Collider2D>().enabled = true;
        _input.LeftDown -= HandleLeftDown;
    }
    // Update is called once per frame
    void Update()
    {
        if(HandScript.Activated) return;

        transform.position = _input.WorldMouse;
    }
}
