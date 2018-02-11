using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    //flags for movement 
    public bool autoplay = false;
    public bool playWithMouse = true;
    //boundaries for right and left
    public float minX;
    public float maxX;
    //Direct reference to the ball, so no finding necessary.
    public Ball ball;
    //paddle offset, to smoothen out the bouncing.
    public float offsetX=0;
    //Contains multiple sound effects for when the paddle is hit.
    public AudioSource[] hyahSoundEffects;
    //Controllers for the links and their respective animation players.
    public animControl[] linkAnimations;

    void Update()
    {
        //Checks for multiple inputs, to switch between autoplay, mouse play and keyboard play.
        if(Input.GetMouseButton(1))
        {
            playWithMouse = true;
            autoplay = false;
        }
        if(Input.GetKey("s"))
        {
            playWithMouse = false;
            autoplay = false;
        }
        if(Input.GetKey("p"))
        {
            autoplay = true;

        }
        if (playWithMouse && !autoplay)
        {
            MoveWithMouse();
        }
        else if(!playWithMouse && !autoplay)
        {
            MoveWithKeyBoard();
            
        }
        else
        {
            AutoPlay();
        }
        
    }
    //Activates controls with the "d" and "a" keys from the keyboard.
    void MoveWithKeyBoard()
    {
        float speed = 10.0f;
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    //Makes the paddle aoutomatically follow the ball with a slight delay.
    void AutoPlay()
    {
        Vector3 paddlePos = this.transform.position;
        Vector3 ballPos = ball.transform.position;
        paddlePos.x = Mathf.Clamp(ball.transform.position.x - 0.5f,minX, maxX);
        this.transform.position = paddlePos;
    }
    //Controls the movement depending on the position of the mouse, limiting it with the boundaries of the screen. (These can be adjusted in the inspector)
    void MoveWithMouse()
    {
        Vector3 paddlePos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        paddlePos.x = Mathf.Clamp(mousePosInBlocks-0.15f, minX, maxX);
        this.transform.position = paddlePos;
    }
    //Uses a collision to play random hit sounds.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(ball.getHasStarted())
        {
            hyahSoundEffects[UnityEngine.Random.Range(0, hyahSoundEffects.Length)].Play();
            for(int i = 0;i<linkAnimations.Length;i++)
            {
                linkAnimations[i].PlayAnim();
            }
        }
    }
}
