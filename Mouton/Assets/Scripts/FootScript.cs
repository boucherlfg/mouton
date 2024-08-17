using System;
using UnityEngine;

public class FootScript : MonoBehaviour {
    public static event Action TouchedGround;
    private Collider2D _collider2D;

    void Start() {
        _collider2D = GetComponent<Collider2D>();
    }

    void Update() {
        //var rect = _colli;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Ground")) return;

        TouchedGround?.Invoke();
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(!other.gameObject.CompareTag("Ground")) return;
        TouchedGround?.Invoke();
    }

}
