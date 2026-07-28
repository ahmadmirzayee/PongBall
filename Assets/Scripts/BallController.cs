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

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveBall += SetBallValues;
        moveBall += PushBall;
        moveBall += ResetBall;
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
        direction = UnityEngine.Random.value < 0.5f ? Vector2.left : Vector2.right;
        rotationSpeed = UnityEngine.Random.value < 0.5f ? 1 : -1;

        angle = UnityEngine.Random.Range(-maxAngle, maxAngle);
        direction.y = angle;
    }

    private void PushBall()
    {
        rigidbody.linearVelocity = direction * speed;
    }

    private void ResetBall()
    {
        transform.position = Vector3.zero;
    }

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

        GameManager.instance.audioManager.PlaySound("score");
        moveBall?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("W"))
        {
            GameManager.instance.audioManager.PlaySound("wall");
            return;
        }

        PlayerController paddle = collision.gameObject.GetComponent<PlayerController>();

        if (paddle.id == 1)
        {
            rotationSpeed = 1;
            //StartCoroutine(GameManager.instance.aiController.selectTarget());
        }
        else if (paddle.id == 2)
        {
            rotationSpeed = -1;
        }

        changeBallAngle(collision);
        GameManager.instance.audioManager.PlaySound("paddle");
        rigidbody.linearVelocityX *= speedMultiplier;
        rotationSpeed *= speedMultiplier;
    }

    private void changeBallAngle(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        float halfHeight = collision.collider.bounds.size.y / 2f;

        float relative = (contact.point.y - collision.transform.position.y) / halfHeight;

        Vector2 newDirection = new Vector2(1, relative).normalized;

        rigidbody.linearVelocityY = newDirection.y * speed;
    }
}
