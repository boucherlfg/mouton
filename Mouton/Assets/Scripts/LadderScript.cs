using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public static int ladderCount;
    public Collider2D platform;
    private float gravity = 0;
    
    
    void OnTriggerEnter2D(Collider2D other) {
        if(!other.GetComponent<MoveScript>()) return;

        ladderCount++;
        if(ladderCount > 1) return;
        other.GetComponent<JumpScript>().isJumping = true;
        other.GetComponent<MoveScript>().YAxisMove = true;

        other.GetComponent<Rigidbody2D>().gravityScale = 0;
        
    }

    void OnTriggerExit2D(Collider2D other) {
        if(!other.GetComponent<MoveScript>()) return;

        ladderCount--;
        if(ladderCount > 0) return;
        other.GetComponent<JumpScript>().isJumping = false;
        other.GetComponent<MoveScript>().YAxisMove = false;
        other.GetComponent<Rigidbody2D>().gravityScale = other.GetComponent<JumpScript>().gravityScale;
    }

}
