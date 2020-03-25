using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartChangeScene : MonoBehaviour
{
    public string scene = "Grave";
    public RawImage whiteImage;
    public float timeToLoadScene = 5f;

    private float lerpTime;
    private Color finalWhiteRGBA;
    private float scale;
    private bool readyToLoadScene = true;

    private void Awake()
    {
        lerpTime = -Time.deltaTime;
        finalWhiteRGBA = whiteImage.color;
        finalWhiteRGBA.a = 1f;
        scale = Time.deltaTime * 0.05f;
    }

    public void LoadScene()
    {
        StartCoroutine(GetToScene(timeToLoadScene));
    }

    protected IEnumerator GetToScene(float timeToLoadScene = 5f, int whenToAccelerate = 400)
    {
        if (readyToLoadScene)
        {
            readyToLoadScene = false;
            while (true)
            {
                WhiteLerp(whenToAccelerate);
                lerpTime += Time.deltaTime;
                if (lerpTime >= timeToLoadScene)
                {
                    SceneManager.LoadSceneAsync(scene);
                    break;
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }

    private int lerpCount = 0;
    private void WhiteLerp(int whenToAccelerate)
    {
        if (lerpCount == whenToAccelerate)
        {
            scale *= 100;
        }
        whiteImage.color = Color.Lerp(whiteImage.color, finalWhiteRGBA, scale);
        lerpCount += 1;
    }
}
