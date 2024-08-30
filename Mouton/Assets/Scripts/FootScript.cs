using System;
using UnityEngine;

public class OnTouchedGround : BaseEvent {}
public class FootScript : MonoBehaviour {
    private Collider2D _collider2D;

    void Start() {
        _collider2D = GetComponent<Collider2D>();
    }

    void Update() {
        //var rect = _colli;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Ground")) return;
        if(LadderScript.ladderCount > 0) return;
        ServiceManager.Instance.Get<OnTouchedGround>().Invoke();
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(!other.gameObject.CompareTag("Ground")) return;
        if(LadderScript.ladderCount > 0) return;
        ServiceManager.Instance.Get<OnTouchedGround>().Invoke();
    }

}
