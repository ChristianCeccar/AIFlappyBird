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
    public Button yesButton;
    public Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * velocity;
        }
        transform.position += Vector3.right * speed * Time.deltaTime;
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
            pipeSpawn.GetComponent<PipeSpawn>().gameOverCheck = true;
            gameOver.SetActive(true);
            yesButton.onClick.AddListener(yesButtonListener);
            noButton.onClick.AddListener(noButtonListener);

            Time.timeScale = 0;
        }
    }
}
