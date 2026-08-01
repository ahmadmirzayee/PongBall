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

    // Initialize the paddle and ball references and set the AI movement values at the start of the game
    private void Start()
    {
        paddle = GetComponent<PlayerController>();
        ball = GameManager.instance.ballController.GetComponent<BallController>();
        SetAiMovementValues();
    }

    // Update the AI's movement based on the ball's position and the AI's movement values
    private void Update()
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

    // Set the AI's movement values based on the combined score of the player and the AI
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

    // This function is used to move the AI's paddle based on the direction and speed values, and reset the time value after a certain amount of time has passed
    public void MoveAi()
    {
        if(time >= reactionTime)
        {
            paddle.rigidbody.linearVelocityY = direction * speed;
            time = 0f;
        }
    }
}
