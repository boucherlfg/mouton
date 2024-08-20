using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour {
    public static bool Activated {get;set;} = true;
    public AudioClip pickupSound;
    public AudioClip throwSound;
    public List<Collider2D> accessible;
    private Collider2D _hovered;
    public Collider2D Hovered {
        get => _hovered;
        set {
            if(_hovered) SetHover(_hovered, false);
            _hovered = value;
            if(_hovered) SetHover(Hovered, true);
        }
    }
    private InputService _input;
    [HideInInspector]
    public Transform carried;
    public float maxHeight = 2;
    public float maxVelocity = 5;
    public float pickupRange = 2;
    [SerializeField]
    private Transform hand;
    void Start() {
        _input = ServiceManager.Instance.Get<InputService>();
        _input.LeftDown += HandleClick;
        _input.RightClick += TakeBackPlatform;
    }

    private void TakeBackPlatform()
    {
        var hovered = Physics2D.OverlapCircleAll(_input.WorldMouse, 0.4f)
                            .Where(x => x.GetComponent<PlatformPickupable>() && (!carried || x.transform != carried.transform))
                            .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();
        if(!hovered) return;
        var platform = hovered.GetComponent<PlatformPickupable>();
        platform.Start();
    }

    void Update() {
        // -------------- ACCESSIBLE
        this.accessible.RemoveAll(x => !x);
        var accessible = Physics2D.OverlapCircleAll(transform.position, pickupRange)
                           .Where(x => x.GetComponent<PickUpable>() && (!carried || x.transform != carried)).ToList();

        // remove those who are too far
        this.accessible.FindAll(a => !accessible.Contains(a)).ForEach(a => SetOutline(a, false));
        this.accessible.RemoveAll(a => !accessible.Contains(a));
        
        // add those who are now close enough
        var toAdd = accessible.FindAll(a => !this.accessible.Contains(a));
        toAdd.ForEach(a => SetOutline(a, true));
        this.accessible.AddRange(toAdd);
        
        // -------------- HOVERED
        if(!this.Hovered) Hovered = null;

        if(!this.accessible.Contains(this.Hovered)) {
            this.Hovered = null;
        }

        var hovered = Physics2D.OverlapCircleAll(_input.WorldMouse, 0.4f)
                            .Where(x => (x.GetComponent<PickUpable>() || x.GetComponent<PlatformPickupable>()) && (!carried || x.transform != carried.transform))
                            .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();
        
        if(hovered && !hovered.GetComponent<PlatformPickupable>() && !accessible.Contains(hovered)) hovered = null;
        Hovered = hovered;
    }

    void SetHover(Collider2D obj, bool active) 
    {
        float size = active ? 1.1f : 1;
        obj.transform.localScale = Vector3.one * size;
    }

    void SetOutline(Collider2D obj, bool active) {
        obj.GetComponentInChildren<OutlineScript>(true).gameObject.SetActive(active);
    }

    void OnDestroy() {
        _input.LeftDown -= HandleClick;
    }

    public void Drop() {
        if(!carried) return;
        carried.transform.parent = null;
        carried.GetComponent<Rigidbody2D>().simulated = true;
        if(carried.TryGetComponent(out KillAfterTime food)) food.Frozen = !food.frozenFromFreezer;
        carried = null;
    }

    void Pickup(Transform pickupable) {
        if(!Activated) return;
        if(carried) {
            carried.GetComponent<Rigidbody2D>().simulated = true;
            
            if(carried.TryGetComponent(out KillAfterTime food2)) food2.Frozen = true;
            carried.transform.parent = null;
            carried = null;
        }

        if(!pickupable) return;

        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        carried = pickupable.transform;
        carried.transform.parent = hand;
        carried.transform.localPosition = Vector2.zero;
        carried.GetComponent<Rigidbody2D>().simulated = false;
        if(carried.TryGetComponent(out KillAfterTime food)) food.Frozen = true;
    }

    void HandleClick() {
        if(!Activated) return;
        if(OnPickedUp()) return;
        if(!this.carried && Hovered && Hovered.GetComponent<PlatformPickupable>()) {
            TakeBackPlatform();
            return;
        }
        if(!this.carried) return;

        // throw stuff
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
  
    bool OnPickedUp() {
        if(carried) return false;
        var mousePos = _input.WorldMouse;
        var closest = Physics2D.OverlapCircleAll(mousePos, 0.4f)
                            .Where(x => x.GetComponent<PickUpable>() && (!carried || x.transform != carried.transform))
                            .OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();

        if(!closest) {
            return false;
        }

        if(!accessible.Contains(closest)) return false;

        Pickup(closest.transform);
        return true;
    }
}
