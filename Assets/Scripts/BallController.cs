using System;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [Header("Move Components")]
    public float speed;
    public float maxAngle;
    public float speedMultiplier;
    private float rotationSpeed;
    private float angle;
    private Rigidbody2D rigidbody;
    private Vector2 direction;

    [Header("Events")]
    public Action moveBall;

    [Header("ID for PowerUp")]
    private int paddleID;

    [Header("Particle system")]
    public GameObject particle;

    // Give rigidbody a RigidBody2D component and assign the moveBall action with required functions at the start of the game
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveBall += SetBallValues;
        moveBall += PushBall;
        moveBall += ResetBall;
    }

    // Start the game by invoking the moveBall action
    private void Start()
    {
        moveBall?.Invoke();
    }

    // Rotate the ball for juice of the game in the Update function
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    // Set the ball's direction, rotation speed, and angle randomly when the game starts or when a player scores
    private void SetBallValues()
    {
        direction = UnityEngine.Random.value < 0.5f ? Vector2.left : Vector2.right;
        rotationSpeed = UnityEngine.Random.value < 0.5f ? 1 : -1;

        angle = UnityEngine.Random.Range(-maxAngle, maxAngle);
        direction.y = angle;
    }

    // Push the ball in the direction of the set values with the speed value
    private void PushBall()
    {
        rigidbody.linearVelocity = direction * speed;
    }

    // Reset the ball's position to the center of the screen when a player scores
    private void ResetBall()
    {
        transform.position = Vector3.zero;
    }

    // When the ball collides with a trigger, check if the toggle is on or off and increase or decrease the score accordingly.
    // If the ball collides with a power-up, start the corresponding coroutine and destroy the power-up object. Play the appropriate sound effect for each collision.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.instance.toggleController.GetComponent<Toggle>().isOn == true)
        {
            if (collision.gameObject.tag == "P1SZ")
            {
                GameManager.instance.scoreManager.IncreaseScore(2);
            }
            else if (collision.gameObject.tag == "P2SZ")
            {
                GameManager.instance.scoreManager.IncreaseScore(1);
            }
        }
        else if(GameManager.instance.toggleController.GetComponent<Toggle>().isOn == false)
        {
            if (collision.gameObject.tag == "P1SZ")
            {
                GameManager.instance.scoreManager.DecreaseScore(2);
            }
            else if (collision.gameObject.tag == "P2SZ")
            {
                GameManager.instance.scoreManager.DecreaseScore(1);
            }
        }

        if(collision.tag == "Multiplier")
        {
            StartCoroutine(GameManager.instance.powerupManager.MultiplyScore());
            Destroy(collision.gameObject);
            GameManager.instance.audioManager.PlaySound("powerup");
        }
        else if (collision.tag == "Minus")
        {
            StartCoroutine(GameManager.instance.powerupManager.MinusScore());
            Destroy(collision.gameObject);
            GameManager.instance.audioManager.PlaySound("powerup");
        }
        else if(collision.tag == "Shield")
        {
            StartCoroutine(GameManager.instance.powerupManager.DoShield(paddleID));
            Destroy(collision.gameObject);
            GameManager.instance.audioManager.PlaySound("powerup");
        }
        else
        {
            GameManager.instance.audioManager.PlaySound("score");
            moveBall?.Invoke();
        }

        Instantiate(particle, transform.position, Quaternion.identity);
    }

    // When the ball collides with a paddle, check which paddle it is and set the rotation speed and paddle ID accordingly.
    // Change the ball's angle based on where it hit the paddle and play the appropriate sound effect.
    // Increase the ball's speed and rotation speed by the speed multiplier.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("W"))
        {
            GameManager.instance.audioManager.PlaySound("wall");
            Instantiate(particle, transform.position, Quaternion.identity);
            return;
        }

        if (collision.gameObject.name.Contains("S"))
        {
            GameManager.instance.audioManager.PlaySound("wall");
            Instantiate(particle, transform.position, Quaternion.identity);
            return;
        }

        PlayerController paddle = collision.gameObject.GetComponent<PlayerController>();
        paddleID = paddle.id;

        if (paddleID == 1)
        {
            rotationSpeed *= 1;
        }
        else if (paddleID == 2)
        {
            rotationSpeed *= -1;
        }

        changeBallAngle(collision);
        GameManager.instance.audioManager.PlaySound("paddle");
        rigidbody.linearVelocityX *= speedMultiplier;
        rotationSpeed *= speedMultiplier;
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    // Change the ball's angle based on where it hit the paddle.
    // The angle is calculated by taking the contact point of the collision and comparing it to the center of the paddle.
    // The new direction is then normalized and applied to the ball's velocity.
    private void changeBallAngle(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        float halfHeight = collision.collider.bounds.size.y / 2f;

        float relative = (contact.point.y - collision.transform.position.y) / halfHeight;

        Vector2 newDirection = new Vector2(1, relative).normalized;

        rigidbody.linearVelocityY = newDirection.y * speed;
    }
}
