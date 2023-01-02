using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenElement
{

    public bool Opened { get; set;}
    public MonoBehaviour MonoBehaviour { get; set; }

    public delegate IEnumerator Method();

    public Method BeforeOpen { get; set; }
    public Method AfterOpen { get; set; }

    public Method BeforeClose { get; set; }
    public Method AfterClose { get; set; }


    public void Configurations();


    public void Open()
    {

        ScreenManager.Instance.StartCoroutine(OpenRoutine());

    }

    private IEnumerator OpenRoutine()
    {
        if(BeforeOpen != null)
            yield return BeforeOpen();
        yield return OpenNow();
        if(AfterOpen != null)
            yield return AfterOpen();
    }

    private IEnumerator OpenNow()
    {
        //Debug.Log("OPEN");
        Opened = true;
        MonoBehaviour.gameObject.SetActive(true);
        MonoBehaviour.transform.SetAsLastSibling();
        yield return null;
    }

    public void Close()
    {
        ScreenManager.Instance.StartCoroutine(CloseRoutine());
    }

    private IEnumerator CloseRoutine()
    {
        if(BeforeClose!=null)
            yield return BeforeClose();
        yield return CloseNow();
        if(AfterClose!=null)
            yield return AfterClose();
    }

    private IEnumerator CloseNow()
    {
        //Debug.Log("CLOSE");
        Opened = false;
        MonoBehaviour.gameObject.SetActive(false);
        yield return null;
    }

}
