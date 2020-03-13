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
    public GameObject getPipe;
    public GameObject pipeData;
    const int numBirdNetwork = 10;
    bool restartS = false;
    public GameObject cam;
    float[] results = new float[numBirdNetwork];
    // Start is called before the first frame update
    void Start()
    {
        birdClass = GameObject.FindWithTag("BirdSpawner");
        birds = initBirds(numBirdNetwork);
        nets = initNetwork(numBirdNetwork);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            softReset();
        }

        for (int i = 0; i < numBirdNetwork; i++)
        {
            birdClass = birds[i];
            pipeData = getPipe.GetComponent<PipeSpawn>().nextPipe(getPipe.GetComponent<PipeSpawn>().pipeList, birds[i]);

            inputs[0] = pipeData.transform.position.y + 1.3f;
            inputs[1] = pipeData.transform.position.y - 1.3f;
            inputs[2] = birds[i].GetComponent<Bird>().transform.position.y;

            //Debug.Log("input 0: " + inputs[0]);
            //Debug.Log("input 1: " + inputs[1]);
            //Debug.Log("input 2: " + inputs[2]);

            nets[i].SetFitness(birds[i].GetComponent<Bird>().score);
            //var xyz = sortNN(nets);
            Debug.Log("score: " + birds[i].GetComponent<Bird>().score);
            if (Time.frameCount % 2250 == 0)
            {
                var sortedNN = sortNN(nets);
                nets[i] = sortedNN[i / 2];
                nets[i].Mutate();
                softReset();
            }
            else
            {

                pipeData = getPipe.GetComponent<PipeSpawn>().nextPipe(getPipe.GetComponent<PipeSpawn>().pipeList, birds[i]);

                results[i] = nets[i].FeedForward(inputs)[0];
                results[i] += 0.5f;
                Debug.Log(results[0]);

            }

            if (results[i] > 0.5f)
            {
                birdClass.GetComponent<Bird>().birdJump();
            }

        }
    }

    List<GameObject> initBirds(int n)
    {
        List<GameObject> birds = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            temp = Instantiate(birdPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            birds.Add(temp);

        }
        return birds;
    }

    List<NeuralNetwork> initNetwork(int n)
    {
        List<NeuralNetwork> nn = new List<NeuralNetwork>();
        for (int i = 0; i < n; i++)
        {
            NeuralNetwork tempNN = new NeuralNetwork(layers);
            nn.Add(tempNN);
        }
        return nn;
    }

    List<NeuralNetwork> sortNN(List<NeuralNetwork> unn)
    {
        NeuralNetwork tempNN;
        for (int p = 0; p < unn.Count - 1; p++)
        {
            for (int i = 0; i < unn.Count - 1; i++)
            {
                if (unn[i].GetFitness() < unn[i + 1].GetFitness())
                {
                    tempNN = unn[i + 1];
                    unn[i + 1] = unn[i];
                    unn[i] = tempNN;
                }
            }
        }
        return unn;
    }

    public void softReset()
    {
        
        for (int i = 0; i < numBirdNetwork; i++)
        {
            birds[i].transform.position = new Vector3(0, 0, 0);
            birds[i].GetComponent<Bird>().score = 0;
        }

        while (getPipe.GetComponent<PipeSpawn>().pipeList.Count > 0)
        {
            Destroy(getPipe.GetComponent<PipeSpawn>().pipeList[0].gameObject);
            getPipe.GetComponent<PipeSpawn>().pipeList.RemoveAt(0);
        }

        cam.GetComponent<CameraMove>().enabled = false;
        cam.GetComponent<CameraMove>().transform.position = new Vector3(0, 1, -10);
        cam.GetComponent<CameraMove>().enabled = true;
    }
}
