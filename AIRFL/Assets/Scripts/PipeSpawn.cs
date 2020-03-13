using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public float maxTimer = 1;
    public float timer = 0;
    public GameObject pipes;
    public float height;
    public GameObject giveBird;
    public List<GameObject> b = new List<GameObject>();
    public bool gameOverCheck = false;
    GameObject temp;
    public List<GameObject> pipeList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        temp = Instantiate(pipes);
        temp.transform.position = transform.position + new Vector3(Camera.main.transform.position.x + 4, Random.Range(-height, height), 0);
        pipeList.Add(temp);
        for (int i = 0; i < giveBird.GetComponent<BirdManager>().birds.Count; i++) {
            b.Add(giveBird.GetComponent<BirdManager>().birds[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameOverCheck == false)
        {
            Time.timeScale = 1;
            if (timer > maxTimer)
            {
                temp = Instantiate(pipes);
                temp.transform.position = transform.position + new Vector3(Camera.main.transform.position.x + 4, Random.Range(-height, height), 0);
                temp.gameObject.name = "Pipe" + Time.frameCount;
                pipeList.Add(temp);
                timer = 0;
            }
            for (int i = 0; i < b.Count; i++)
            {
                //Debug.Log(nextPipe(pipeList, b[i]).gameObject.name);
            }

            timer += Time.deltaTime;
        }

    }

    public GameObject nextPipe(List<GameObject> p, GameObject b)
    {

        for(int i = 0; i < p.Count; i++)
        {
             float dist = p[i].transform.position.x - b.transform.position.x;
           
            if (dist > 0)
            {
                return p[i];
            }
        }

        return null;
    }
}
