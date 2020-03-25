using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;

public class ForceReleaseGradedObject : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject RightHand;
    public string scene = "Final";
    public RawImage whiteImage = null;
    //----------------------------------------------------------
    private VRTK_InteractGrab grabInformationLeft = null;
    private VRTK_InteractGrab grabInformationRight = null;
    private VRTK_InteractGrab grabInformation = null;
    private GameObject grabObject = null;
    private string grabObjectName = null;
    private float grabbingTime;
    private float fireEndureTimeLimit = 3f;
    private bool hasGrabbed = false;
    private bool readyToLoadScene = false;
    private float afterForceReleaseTime;
    private float timeToLoadScene = 5f;
    private Color finalWhiteRGBA;
    private float scale;

    private void Awake()
    {
        grabInformationLeft = LeftHand.GetComponent<VRTK_InteractGrab>();
        grabInformationRight = RightHand.GetComponent<VRTK_InteractGrab>();
        grabbingTime = -Time.deltaTime;
        afterForceReleaseTime = -Time.deltaTime;
        finalWhiteRGBA = whiteImage.color;
        finalWhiteRGBA.a = 1f;
        scale = Time.deltaTime * 0.1f;
    }   

    private void Update()
    {
        grabObject = grabInformationLeft.GetGrabbedObject() == null ?
                         grabInformationRight.GetGrabbedObject() : grabInformationLeft.GetGrabbedObject();
        grabInformation = grabInformationLeft.GetGrabbedObject() == null ?
                              grabInformationRight : grabInformationLeft;
        if (grabObject != null)
        {
            if (grabbingTime <= 0.000001f)
            {
                grabObjectName = grabObject.name;
                Debug.Log("Start grabbing: " + grabObjectName);
                hasGrabbed = true;
            }

            grabbingTime += Time.deltaTime;
            if (grabbingTime >= fireEndureTimeLimit)
            {
                grabInformation.ForceRelease();
                readyToLoadScene = true;
            }
        }

        if(hasGrabbed && grabObject == null)
        {
            Debug.Log("Stop grabbing: " + grabObjectName);
            grabObjectName = null;
            hasGrabbed = false;
            grabbingTime = -Time.deltaTime;
        }

        TryToLoadScene(readyToLoadScene);
    }

    public GameObject GetGrabbedObject()
    {
        return grabObject;
    }

    private void TryToLoadScene(bool readyToLoadScene)
    {
        if (readyToLoadScene)
        {
            WhiteLerp();
            afterForceReleaseTime += Time.deltaTime;
            if (afterForceReleaseTime >= timeToLoadScene)
            {
                SceneManager.LoadSceneAsync(scene);
                readyToLoadScene = false;
            }
        }
    }

    private int lerpCount = 0;
    private void WhiteLerp()
    {
        if (lerpCount == 400)
        {
            scale *= 100;
        }
        whiteImage.color = Color.Lerp(whiteImage.color, finalWhiteRGBA, scale);
        lerpCount += 1;
    }
}
