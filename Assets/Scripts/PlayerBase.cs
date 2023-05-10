using ScreenManagerNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBase : MonoBehaviour
{



    private void OnMouseDown()
    {
        if (CursorOnUI)
        {
            return;
        }
        ScreenManager.Instance.OpenPopUp("BasePopUp");
    }

    public bool CursorOnUI
    {
        get
        {
#if UNITY_ANDROID && !UNITY_EDITOR
    if (EventSystem.current.IsPointerOverGameObject())
        return true;
 
    for (int touchIndex = 0; touchIndex < Input.touchCount; touchIndex++)
    {
        Touch touch = Input.GetTouch(touchIndex);
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return true;
    }
 
    return false;
#endif
            return EventSystem.current.IsPointerOverGameObject();
        }

    }

}
