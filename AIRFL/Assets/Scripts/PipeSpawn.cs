using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public float maxTimer = 1;
    public float timer = 0;
    public GameObject pipes;
    public float height;
    public Bird b;
    public bool gameOverCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = Instantiate(pipes);
        temp.transform.position = transform.position + new Vector3(b.transform.position.x + 4, Random.Range(-height, height), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverCheck == false)
        {
            Time.timeScale = 1;
            if (timer > maxTimer)
            {
                GameObject temp = Instantiate(pipes);
                temp.transform.position = transform.position + new Vector3(b.transform.position.x + 4, Random.Range(-height, height), 0);
                Destroy(temp, 10);
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }
}
