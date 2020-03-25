using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChangeScene : StartChangeScene
{
    public float finalTime = 15f;

    private float startTime;

    private void Awake()
    {
        this.scene = "Start";
        this.timeToLoadScene = 1f;
    }

    private void Start()
    {
        Debug.Log("Final Scene !");
        startTime = Time.time;
    }

    private void Update()
    {
        if ((Time.time - startTime) >= finalTime)
        {
            StartCoroutine(GetToScene(timeToLoadScene));
        }
    }
}
