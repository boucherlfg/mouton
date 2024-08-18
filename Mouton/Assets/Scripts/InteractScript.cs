using System.Linq;
using UnityEngine;

public class InteractScript : MonoBehaviour {
    private InputService _inputService;
    void Start() {
        _inputService = ServiceManager.Instance.Get<InputService>();
        _inputService.Interacted += OnInteracted;
    }


    void OnDestroy() {
        _inputService.Interacted -= OnInteracted;
    }
    
    void OnInteracted() {
        var choice = Physics2D.OverlapCircleAll(transform.position, 1)
                            .Where(x => x.GetComponent<CraftingTable>())
                            .OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
                            .FirstOrDefault();
        if(!choice) return;

        choice.GetComponent<CraftingTable>().Interact();
    }
}