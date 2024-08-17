using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPickupable : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    private InputService _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = ServiceManager.Instance.Get<InputService>();
        HandScript.Activated = false;
        _input.LeftDown += HandleLeftDown;
    }

    void HandleLeftDown() {
        HandScript.Activated = true;
        _input.LeftDown -= HandleLeftDown;
    }
    // Update is called once per frame
    void Update()
    {
        if(HandScript.Activated) return;

        var mouse = _input.WorldMouse;
        transform.position = Vector2.Lerp(transform.position, mouse, Time.deltaTime * speed);
    }
}
