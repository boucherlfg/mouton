using System;
using UnityEngine;

public class FootScript : MonoBehaviour {
    public static event Action TouchedGround;

    void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Ground")) return;

        TouchedGround?.Invoke();
    }
}