using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class AiController : MonoBehaviour
{
    [Header("Refrence To Paddle")]
    private PlayerController paddle;
    private BallController ball;

    [Header("AI Movement Values")]
    private float speed;
    private float deadZone;
    public float accuracy;
    private float reactionTime;
    private float direction;
    private float time;

    void Start()
    {
        paddle = GetComponent<PlayerController>();
        ball = GameManager.instance.ballController.GetComponent<BallController>();
        SetAiMovementValues();
    }

    void Update()
    {
        float error = UnityEngine.Random.Range(-accuracy, accuracy);
        float distance = (ball.transform.position.y + error) - transform.position.y;

        time += Time.deltaTime;

        if (MathF.Abs(distance) > deadZone)
        {
            direction = distance > 0 ? 1 : -1;
        }
        else
        {
            direction = 0;
        }
    }

    public void SetAiMovementValues()
    {
        int aiScore = GameManager.instance.scoreManager.playerTwoScore;
        int playerScore = GameManager.instance.scoreManager.playerOneScore;

        if (playerScore +  aiScore <= 5)
        {
            speed = 1.6f;
            deadZone = 1f;
            accuracy = 0.5f;
            reactionTime = 0.5f;
        }
        else if (playerScore + aiScore <= 10)
        {
            speed = 1.8f;
            deadZone = 0.8f;
            accuracy = 0.4f;
            reactionTime = 0.4f;
        }
        else if (playerScore + aiScore <= 15)
        {
            speed = 2f;
            deadZone = 0.6f;
            accuracy = 0.3f;
            reactionTime = 0.3f;
        }
        else if (playerScore + aiScore <= 20)
        {
            speed = 2.2f;
            deadZone = 0.4f;
            accuracy = 0.2f;
            reactionTime = 0.2f;
        }
        else if (playerScore + aiScore <= 25)
        {
            speed = 2.4f;
            deadZone = 0.2f;
            accuracy = 0.1f;
            reactionTime = 0f;
        }
    }

    public void MoveAi()
    {
        print($"speed: {speed}, deadzone: {deadZone}, accuracy: {accuracy}, time: {reactionTime}");

        if(time >= reactionTime)
        {
            paddle.rigidbody.linearVelocityY = direction * speed;
            time = 0f;
        }
    }
}
