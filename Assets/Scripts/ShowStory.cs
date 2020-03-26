using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ShowStory : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<VRTK_DestinationMarker>() == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerPointerEvents_ListenerExample", "VRTK_DestinationMarker", "the Controller Alias"));
            return;
        }

        GetComponent<VRTK_DestinationMarker>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
        //GetComponent<VRTK_DestinationMarker>().DestinationMarkerHover += new DestinationMarkerEventHandler(DoPointerHover);
        GetComponent<VRTK_DestinationMarker>().DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
        //GetComponent<VRTK_DestinationMarker>().DestinationMarkerSet += new DestinationMarkerEventHandler(DoPointerDestinationSet);
    }

    private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
    {
        if (e.target.gameObject.layer == 8)
        {
            e.target.GetComponent<TellStory>().ShowStory();
        }
    }

    private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
    {
        if (e.target.gameObject.layer == 8)
        {
            e.target.GetComponent<TellStory>().HideStory();
        }   
    }

    //private void DoPointerHover(object sender, DestinationMarkerEventArgs e)
    //{
    //}

    //private void DoPointerDestinationSet(object sender, DestinationMarkerEventArgs e)
    //{
    //}

}
