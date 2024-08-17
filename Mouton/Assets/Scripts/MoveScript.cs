using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D _rigidBody;
    private InputService _inputService;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _inputService = ServiceManager.Instance.Get<InputService>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = (_inputService.Move * Vector2.right).normalized;
        _rigidBody.velocity = move * speed + _rigidBody.velocity.y * Vector2.up;
    }
}
