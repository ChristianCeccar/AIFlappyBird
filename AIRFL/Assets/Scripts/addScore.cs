using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addScore : MonoBehaviour
{
    public GameObject s;
    // Start is called before the first frame update
    void Start()
    {
        //s = GameObject.FindWithTag("score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //s.GetComponent<UpdateScore>().score++;
    }
}
