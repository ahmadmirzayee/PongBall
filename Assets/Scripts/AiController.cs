using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class AiController : MonoBehaviour
{
    [Header("Refrence To Paddle")]
    private PlayerController paddle;

    [Header("AI Movement Values")]
    private float speed = 1f;
    private float deadZone = 0.5f;
    private float accuracy = 1f;
    private float reaction = 1f;
    private float direction;

    void Start()
    {
        paddle = GetComponent<PlayerController>();
    }

    void Update()
    {
        direction = Mathf.Sign(GameManager.instance.ballController.transform.position.y - transform.position.y);
    }

    public IEnumerator MoveAi()
    {
        
        yield return new WaitForSeconds(reaction);

        paddle.rigidbody.linearVelocityY = direction * speed;

        StopAllCoroutines();
    }
}
