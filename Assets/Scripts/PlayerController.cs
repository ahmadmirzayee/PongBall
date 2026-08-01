using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    // Initialize the input system
    private void Awake()
    {
        input = new PlayerInput();
    }

    // Enable the input system when the object is enabled
    private void OnEnable()
    {
        input.Enable();
    }

    // Disable the input system when the object is disabled
    private void OnDisable()
    {
        input.Disable();
    }

    // Update the player's movement based on the input system and AI controller
    private void Update()
    {
        if(id == 2 && GameManager.instance.toggleController.GetComponent<Toggle>().isOn == false)
        {
            GameManager.instance.aiController.MoveAi();
        }
        else
        {
            Move();
        }
    }

    // Move the player based on the input system's values for Player One or Player Two
    private void Move()
    {
        if (id == 1)
        {
            coordinate = input.PlayerOne.Move.ReadValue<Vector2>();
        }
        else if (id == 2)
        {
            coordinate = input.PlayerTwo.Move.ReadValue<Vector2>();
        }

        rigidbody.linearVelocity = coordinate * speed;
    }
}
