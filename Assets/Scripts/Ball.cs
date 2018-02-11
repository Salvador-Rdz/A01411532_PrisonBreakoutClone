using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {
    //Flag for game start
    bool hasStarted = false;
    //Reference to a paddle.
    public Paddle paddle;
    //Vector useful for handling the position of the ball relative to the paddle.
    private Vector3 paddleToBallVector;

    void Start()
    {
        paddleToBallVector = this.transform.position - paddle.transform.position;
    }
    //Sticks the ball to the padle if the player hasn't shot the ball, once they do, it starts the game and applies force to it.
    void Update()
    {
        if(!hasStarted)
        {
            this.transform.position = paddle.transform.position + paddleToBallVector;
            if(Input.GetMouseButton(0))
            {
                hasStarted = true;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
            }
        }
    }
    //Manages the speed gaining of the ball on collision.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0.2f,0.4f),Random.Range(0.2f,0.4f));
        if(hasStarted)
        {
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<Rigidbody2D>().velocity += tweak;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0.2f, 0.4f), Random.Range(0.2f, 0.4f)));
        }
    }
    //Useful for checking status of the game, the two methods are used in the Lose collider to reset the game if hit.
    public bool getHasStarted()
    {
        return hasStarted;
    }
    public void ResetBall()
    {
        hasStarted = false;
    }
}
