using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour,IScreenElement
{

    public bool Opened { get; protected set; }

    public virtual void Open()
    {

        Opened = true;
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {

        Opened=false;
        gameObject.SetActive(false);
    }

}
