using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Move Components")]
    public float speed;
    public float minAngle;
    public float maxAngle;
    public float speedMultiplier;
    private float rotationSpeed;
    private float angle;
    private Rigidbody2D rigidbody;
    private Vector2 direction;

    [Header("Events")]
    private Action moveBall;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveBall += SetBallValues;
        moveBall += PushBall;
    }

    void Start()
    {
        moveBall?.Invoke();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void SetBallValues()
    {
        if (UnityEngine.Random.value < 0.5f)
        {
            direction = Vector2.left;
            rotationSpeed = 1;
        }
        else
        {
            direction = Vector2.right;
            rotationSpeed = -1;
        }

        angle = UnityEngine.Random.Range(minAngle, maxAngle);
        direction.y = angle;
    }

    private void PushBall()
    {
        rigidbody.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "P1SZ")
        {
            GameManager.instance.scoreManager.IncreaseScore(2);
        }
        else if(collision.gameObject.tag == "P2SZ")
        {
            GameManager.instance.scoreManager.IncreaseScore(1);
        }

        transform.position = Vector3.zero;
        moveBall?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("W"))
        {
            return;
        }

        PlayerController paddle = collision.gameObject.GetComponent<PlayerController>();

        if (paddle.id == 1)
        {
            rotationSpeed = 1;
        }
        else if (paddle.id == 2)
        {
            rotationSpeed = -1;
        }

        rigidbody.linearVelocityX *= speedMultiplier;
        rotationSpeed *= speedMultiplier;
    }
}
