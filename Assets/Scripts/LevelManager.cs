using UnityEngine;
using UnityEngine.SceneManagement;		// Requiered to switch scenes
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static LevelManager levelManager;
	public static AudioManager audioManager;
    public int Bricks = Brick.breakableCount;
    public int CurrentSceneIndex;

    // Use this for initialization
    void Awake()
    {
        if (levelManager == null)
        {
            DontDestroyOnLoad(gameObject);
            levelManager = this;
        }
        else if (levelManager != this)
        {
            Destroy(gameObject);
        }
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void LoadLevel(string name)
    {
        print("Loading " + name);
        SceneManager.LoadScene(name);
        if (name =="level_01")
        {
            audioManager.Play("bgLevelMusic");
        }
    }

    public void EndGame(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel()
    {
        Brick.breakableCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void BrickDestroyed()
    {
        if (Brick.breakableCount == 0 && SceneManager.GetActiveScene().buildIndex !=12) LoadNextLevel();
    }

    private void Update()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Bricks = Brick.breakableCount;
        //For debugging purposes
        if(SceneManager.GetActiveScene().buildIndex ==10)
        {
            audioManager.StopAll();
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 11)
        {
            audioManager.StopAll();
            Brick.breakableCount = 0;
        }
        if (Input.GetKey("n"))
        {
            Brick.breakableCount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void LoadStart()
    {
        SceneManager.LoadScene(0);
    }
}
