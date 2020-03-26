using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public GameObject fireManager;

    private Material material;
    private Color targetColor;
    private Vector3 originPosition;
    private Quaternion originQuaternion;
    private bool isFading = false;

    private void Awake()
    {
        //targetColor = new Color(material.color.r, material.color.g, material.color.b, 0f);
        originPosition   = transform.position;
        originQuaternion = transform.rotation;
        fireManager = GameObject.Find("ForceReleaseManager");
    }

    public void FadeFireOutAndReturn()
    {
        Debug.Log("Fade out !");
        StartCoroutine(FadeProcess());
    }

    private float fadedTime = 0f;
    private IEnumerator FadeProcess()
    {
        isFading = true;
        while (fadedTime < fadeDuration)
        {
            //material.color = Color.Lerp(material.color, targetColor, 0.5f);
            yield return new WaitForSeconds(Time.deltaTime);
            fadedTime += Time.deltaTime;
        }

        fadedTime = 0f;
        ForceReleaseGrabbedObject.GetGrabInformation().ForceRelease();
        isFading = false;
        transform.position = originPosition;
        transform.rotation = originQuaternion;
        Debug.Log("Return !");
    }

    public bool GetIsFading()
    {
        return isFading;
    }
}
