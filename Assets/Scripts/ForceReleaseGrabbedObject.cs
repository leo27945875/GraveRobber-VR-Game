using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;

public class ForceReleaseGrabbedObject : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject fire;
    public float fireEndureTimeLimit = 3f;
    public int grabCountLimit = 3;
    public string scene = "Final";
    public RawImage whiteImage;

    private VRTK_InteractGrab grabInformationLeft;
    private VRTK_InteractGrab grabInformationRight;
    private static VRTK_InteractGrab grabInformation;
    private static GameObject grabObject;
    private float grabbingTime;
    private FireController fireController;
    

    private bool readyToLoadScene = true;
    private float afterForceReleaseTime;
    private float timeToLoadScene = 5f;
    private Color finalWhiteRGBA;
    private float scale;
    private bool hasGrabbed = false;
    private int grabCount = 0;
    

    private void Awake()
    {
        grabInformationLeft = leftHand.GetComponent<VRTK_InteractGrab>();
        grabInformationRight = rightHand.GetComponent<VRTK_InteractGrab>();
        fireController = fire.GetComponent<FireController>();
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
            if (grabbingTime < 0f)
            {
                hasGrabbed = true;
            }

            grabbingTime += Time.deltaTime;
            if (grabbingTime >= fireEndureTimeLimit && !fireController.GetIsFading())
            {
                fireController.FadeFireOutAndReturn();
                grabCount += 1;
                Debug.Log("" + grabCount);
            }
        }
        else if (hasGrabbed && grabObject == null)
        {
            grabbingTime = -Time.deltaTime;
            hasGrabbed = false;
        }

        if (grabCount >= grabCountLimit)
        {
            //TryToLoadScene(readyToLoadScene);
        }
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

    public static GameObject GetGrabbedObject()
    {
        return grabObject;
    }

    public static VRTK_InteractGrab GetGrabInformation()
    {
        return grabInformation;
    }

}
