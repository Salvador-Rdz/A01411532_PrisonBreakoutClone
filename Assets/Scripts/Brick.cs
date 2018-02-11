using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public AudioClip crack;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;
    public GameObject smoke;
    //Counts how many time the entity has been hit
    private int timesHit;
    private LevelManager levelManager;
    //checks for "Breakable" tag
    private bool isBreakable;


	// Use this for initialization
	void Start ()
    {
        isBreakable = (this.tag == "Breakable");
        if(isBreakable)
        {
            breakableCount++;
        }
        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

    //Handles the collision with the bricks, plays a cracking sound and if it has a Breakable tag, it updates per hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position,0.8f);
        if(isBreakable)
        {
            HandleHits();
        }
    }
    //updates the sprites to show wearing, and adds smoke if it gets destroyed.
    void HandleHits()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if(timesHit>= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }
    //Handles the smoke instantiation and coloring to match this object's.
    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity);
        ParticleSystem ps = smokePuff.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex]!=null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Brick Sprite missing");
        }
    }
}
