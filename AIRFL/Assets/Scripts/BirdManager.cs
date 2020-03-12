using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public GameObject birdPrefab;
    GameObject temp;
    public List<GameObject> birds = new List<GameObject>();
    public GameObject birdClass;
    public int generationNum = 0;
    public int[] layers = new int[] { 3, 5, 5, 1 };
    public List<NeuralNetwork> nets;
    float[] inputs = new float[3];//0 = height of pipe 1 = lower height pipe 2 = height of bird


    // Start is called before the first frame update
    void Start()
    {
        birdClass = GameObject.FindWithTag("BirdSpawner");
        for(int i = 0; i < 3; i++)
        {
            temp = Instantiate(birdPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            birds.Add(temp);
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birdClass = birds[i];

            if (Input.GetKeyDown(KeyCode.UpArrow) && i == 0)
            {
                birdClass.GetComponent<Bird>().birdJump();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && i == 1)
            {
                birdClass.GetComponent<Bird>().birdJump();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && i == 2)
            {
                birdClass.GetComponent<Bird>().birdJump();
            }
        }
    }
}
