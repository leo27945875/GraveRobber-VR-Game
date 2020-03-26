using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TellStory : MonoBehaviour
{
    public GameObject storyContentObject;

    private void Awake()
    {
        storyContentObject.SetActive(false);
    }

    public void ShowStory()
    {
        storyContentObject.SetActive(true);
    }

    public void HideStory()
    {
        storyContentObject.SetActive(false);
    }
}
