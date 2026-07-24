using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Identification")]
    public int id;

    [Header("Input System")]
    private PlayerInput input;

    [Header("Move Components")]
    public Rigidbody2D rigidbody;
    public float speed;
    private Vector2 coordinate;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(id == 1)
        {
            coordinate = input.PlayerOne.Move.ReadValue<Vector2>();
        }
        else if(id == 2)
        {
            coordinate = input.PlayerTwo.Move.ReadValue<Vector2>();
        }
        
        rigidbody.linearVelocity = coordinate * speed;
    }
}
