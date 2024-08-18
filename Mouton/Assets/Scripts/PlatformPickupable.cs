using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPickupable : MonoBehaviour
{
    private bool placed = false;
    private InputService _input;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<OutlineScript>(true).gameObject.SetActive(true);
        _input = ServiceManager.Instance.Get<InputService>();
        HandScript.Activated = false;

        ActivateColliders(false);
        _input.LeftDown += HandleLeftDown;
    }
    void ActivateColliders(bool activated) {
        GetComponents<Collider2D>().ToList().ForEach(c => c.enabled = activated);
        GetComponentsInChildren<Collider2D>().ToList().ForEach(c => c.enabled = activated);
    }

    void HandleLeftDown() {
        placed = true;
        HandScript.Activated = true;
        ActivateColliders(true);
        _input.LeftDown -= HandleLeftDown;
        GetComponentInChildren<OutlineScript>(true).gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(placed) return;

        transform.position = _input.WorldMouse;
    }
}
