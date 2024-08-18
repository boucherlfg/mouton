using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private float gravity = 0;

    void OnTriggerEnter2D(Collider2D other) {
        if(!other.GetComponent<MoveScript>()) return;

        other.GetComponent<JumpScript>().isJumping = true;
        other.GetComponent<MoveScript>().YAxisMove = true;

        gravity = other.GetComponent<Rigidbody2D>().gravityScale;
        other.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void OnTriggerExit2D(Collider2D other) {
        if(!other.GetComponent<MoveScript>()) return;

        other.GetComponent<JumpScript>().isJumping = false;
        other.GetComponent<MoveScript>().YAxisMove = false;

        other.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }

}
