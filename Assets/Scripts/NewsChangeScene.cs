using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsChangeScene : StartChangeScene
{
    public float newsTime = 15f;

    private float startTime;

    private void Awake()
    {
        this.scene = "Grave";
        this.timeToLoadScene = 1f;
    }

    private void Start()
    {
        Debug.Log("Start play news !");
        startTime = Time.time;
    }

    private void Update()
    {
        if ((Time.time - startTime) >= newsTime)
        {
            StartCoroutine(GetToScene(timeToLoadScene));
        }
    }
}
