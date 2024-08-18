using System.Linq;
using UnityEngine;

public class InteractScript : MonoBehaviour {
    private Collider2D closest;
    private InputService _inputService;
    void Start() {
        _inputService = ServiceManager.Instance.Get<InputService>();
        _inputService.Interacted += OnInteracted;
    }

    void Update() {
        var closest = Physics2D.OverlapCircleAll(transform.position, 1)
                            .Where(x => x.GetComponent<CraftingTable>())
                            .OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
                            .FirstOrDefault();
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
        _inputService.Interacted -= OnInteracted;
    }
    
    void OnInteracted() {
        
        if(!closest) return;

        closest.GetComponent<CraftingTable>().Interact();
    }
}