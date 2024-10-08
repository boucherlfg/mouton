using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class PlatformPickupable : MonoBehaviour
{
    private bool placed = false;
    private InputService _input;
    // Start is called before the first frame update
    public void Start()
    {
        placed = false;
        ActivateOutline(true);
        _input = ServiceManager.Instance.Get<InputService>();
        ServiceManager.Instance.Get<OnPlatformPlacement>().Invoke(true);

        ActivateColliders(false);
        _input.LeftDown += HandleLeftDown;
        FindObjectOfType<HandScript>().Drop();
    }

    void ActivateColliders(bool activated) {
        GetComponents<Collider2D>().ToList().ForEach(c => c.enabled = activated);
        GetComponentsInChildren<Collider2D>().ToList().ForEach(c => c.enabled = activated);
    }
    public void ActivateOutline(bool activated) {
        
        GetComponentInChildren<OutlineScript>(true).gameObject.SetActive(activated);
    }
    void HandleLeftDown() {
        placed = true;
        ServiceManager.Instance.Get<OnPlatformPlacement>().Invoke(false);
        ActivateColliders(true);
        _input.LeftDown -= HandleLeftDown;
        ActivateOutline(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(placed) return;

        transform.position = _input.WorldMouse;
    }
}
