using System.Linq;
using UnityEngine;

public class HandScript : MonoBehaviour {
    public static bool Activated {get;set;} = true;
    private InputService _input;
    [HideInInspector]
    public Transform carried;
    public float maxHeight = 2;
    public float maxVelocity = 5;
    [SerializeField]
    private Transform hand;
    void Start() {
        _input = ServiceManager.Instance.Get<InputService>();
        _input.PickedUp += OnPickedUp;
        _input.LeftDown += HandleThrow;
    }

    void OnDestroy() {
        _input.PickedUp -= OnPickedUp;
        _input.LeftDown -= HandleThrow;
    }

    void Pickup(Transform pickupable) {
        if(!Activated) return;
        if(carried) {
            carried.GetComponent<Rigidbody2D>().simulated = true;
            carried.transform.parent = null;
            carried = null;
        }

        if(!pickupable) return;

        carried = pickupable.transform;
        carried.transform.parent = hand;
        carried.transform.localPosition = Vector2.zero;
        carried.GetComponent<Rigidbody2D>().simulated = false;
    }

    void HandleThrow() {
        if(!Activated) return;
        if(!this.carried) return;

        var carried = this.carried;
        Pickup(null);
        carried.GetComponent<ItemInteractivity>().Activated = true;
        var mousePos = _input.WorldMouse;
        var delta =  mousePos - (Vector2)transform.position;
        var angle = Mathf.Atan2(delta.y, delta.x);
        var v0 = Mathf.Min(maxVelocity, Ext.GetTrajectoryInitialVelocity(maxHeight, angle));
        var body = carried.GetComponent<Rigidbody2D>();
        body.velocity = v0 * delta.normalized;
        
    }
  
    void OnPickedUp() {
        if(!Activated) return;

        var hit = Physics2D.OverlapCircleAll(transform.position, 0.5f)
                           .Where(x => x.GetComponent<PickUpable>() && (!carried || x.transform != carried.transform))
                           .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();
        if(!hit) {
            Pickup(null);
            return;
        }

        Pickup(hit.transform);
    }
}