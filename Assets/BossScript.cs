using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {
    //Mostly reutilizes a lot of the Brick functionallity and usage with a few changes.
    public AudioClip crack;
    public static int breakableCount = 0;
    public GameObject smoke;
    public float minX;
    public float maxX;
    //Values for hp and Movement of the boss.
    public int maxHits = 50;
    public float speed = 0.1f;
    //label for displaying boss health;
    public Text hpText;
    //Control variables
    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable;
   

    // Use this for initialization
    void Start()
    {
        isBreakable = (this.tag == "Breakable");
        if (isBreakable)
        {
            breakableCount++;
        }
        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        hpText.text = "Boss HP :" + maxHits + " /50";
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Updates the text label
        hpText.text = "Boss HP :" + (maxHits-timesHit) + " /50";
        //Makes the boss bounce left and right when hitting the edges of the screen
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            //Inverts the speed, hence, inverting the movement.
            speed = speed * -1;
        }
        //Updating position
        Vector3 position = new Vector3(transform.position.x + speed, transform.position.y,transform.position.z);
        transform.position = position;
	}
    //Same functionality as Brick's collision trigger.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position, 0.8f);
        if (isBreakable)
        {
            HandleHits();
        }
    }
    //Same functionality as Brick's
    void HandleHits()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            breakableCount = 0;
            levelManager.LoadNextLevel();
            Destroy(gameObject);
        }
        else
        {
            PuffSmoke();
        }
    }
    //Same functionality as Brick's
    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity);
        ParticleSystem ps = smokePuff.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
}
