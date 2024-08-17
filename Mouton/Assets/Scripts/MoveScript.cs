using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D _rigidBody;
    private InputService _inputService;

    public bool YAxisMove {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _inputService = ServiceManager.Instance.Get<InputService>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = _inputService.Move.normalized;
        var xMove = (move.x * Vector2.right).normalized.x;
        var yMove = YAxisMove ? move.y * speed : _rigidBody.velocity.y;
        
        _rigidBody.velocity = xMove * speed * Vector2.right + yMove * Vector2.up;
    }
}
