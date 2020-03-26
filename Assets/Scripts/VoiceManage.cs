using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VoiceManage : MonoBehaviour
{
    public VRTK_ControllerEvents controllerEvents;

    private void Update()
    {
        if (controllerEvents.GetTouchpadAxis() != new Vector2(0f, 0f))
        {
            Debug.Log("Play the Audio !");
        }
    }
}
