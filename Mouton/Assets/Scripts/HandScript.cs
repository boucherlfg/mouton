using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour {
    public static bool Activated {get;set;} = true;
    public AudioClip pickupSound;
    public AudioClip throwSound;
    public Collider2D closest;
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

    void Update() {
        var closest = Physics2D.OverlapCircleAll(transform.position, 0.5f)
                           .Where(x => x.GetComponent<PickUpable>() && (!carried || x.transform != carried.transform))
                           .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();
        if(this.closest && closest != this.closest) {
             this.closest.transform.localScale = Vector3.one;
             this.closest.GetComponentInChildren<OutlineScript>(true).gameObject.SetActive(false);
        }
        
        if(this.closest == closest) return;
        this.closest = closest;
        if(!this.closest) return;

        this.closest.transform.localScale = Vector3.one * 1.1f;
        this.closest.GetComponentInChildren<OutlineScript>(true).gameObject.SetActive(true);
    }

    void OnDestroy() {
        _input.PickedUp -= OnPickedUp;
        _input.LeftDown -= HandleThrow;
    }

    void Pickup(Transform pickupable) {
        if(!Activated) return;
        if(carried) {
            carried.GetComponent<Rigidbody2D>().simulated = true;
            
            if(carried.TryGetComponent(out Ingredient food2)) food2.Frozen = true;
            carried.transform.parent = null;
            carried = null;
        }

        if(!pickupable) return;

        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        carried = pickupable.transform;
        carried.transform.parent = hand;
        carried.transform.localPosition = Vector2.zero;
        carried.GetComponent<Rigidbody2D>().simulated = false;
        if(carried.TryGetComponent(out Ingredient food)) food.Frozen = true;
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
        
        
        if(!closest) {
            Pickup(null);
            return;
        }

        Pickup(closest.transform);
    }
}
