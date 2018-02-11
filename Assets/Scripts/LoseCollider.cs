using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCollider : MonoBehaviour {
    //References
    public LevelManager levelManager;
    public AudioSource ballOut;
    public Text livesText;
    private Ball ball;
    //Life counter
    public int lives = 3;
    //Gets references to the ball and level Manager for handling the lose states.
    void Awake()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        livesText.text = "Lives " + lives;
    }
    private void Update()
    {

    }
    //If hit, plays a sound, decreases the lives resets the ball, and if there are no more lives, loads the lost scene.
    public void OnTriggerEnter2D()
    {
        ballOut.Play();   
        lives--;
        livesText.text = "Lives " + lives;
        if(lives==0)
        {
            levelManager.LoadLevel("Lost");
        }
        else
        {
            ball.ResetBall();
        }   
    }
}
