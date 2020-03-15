using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public float velocity;
    private Rigidbody rb;
    public float speed;
    public GameObject gameOver;
    public GameObject pipeSpawn;
    public GameObject birdM;
    public Button yesButton;
    public Button noButton;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pipeSpawn = GameObject.FindWithTag("Pipe");
        gameOver = GameObject.Find("GameOver");
        birdM = GameObject.Find("BirdSpawner");
        rb.useGravity = true;

    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.y < -6)
        {
            this.gameObject.SetActive(false);
        }
        if (transform.position.y > 6)
        {
            this.gameObject.SetActive(false);
        }
    }

    void yesButtonListener()
    {
        SceneManager.LoadScene(0);
    }

    void noButtonListener()
    {
        Application.Quit();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            
            this.gameObject.SetActive(false);
            //birdM.GetComponent<BirdManager>().softReset();
            //pipeSpawn.GetComponent<PipeSpawn>().gameOverCheck = true;
            //gameOver.SetActive(true);
            //yesButton.onClick.AddListener(yesButtonListener);
            //noButton.onClick.AddListener(noButtonListener);
            //Debug.Log("TEST");
            //Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "scoreCollider")
        {
            score++;
        }
        
    }

    public void birdJump()
    {
        rb.velocity = Vector2.up * velocity;
    }
}

    
