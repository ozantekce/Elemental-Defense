using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpTest : PopUp
{


    // Start is called before the first frame update
    void Start()
    {
        AfterOpen = afterOpen;
        BeforeOpen = beforeOpen;

        BeforeClose = beforeClose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator beforeOpen()
    {
        Debug.Log("before open start");
        transform.localScale = Vector3.zero;
        yield return null;
        Debug.Log("before open over");
    }

    private IEnumerator afterOpen()
    {
        Debug.Log("after open start");
        Tween _tweenerOpen = transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.7f);
        yield return new WaitForEndOfFrame();
        while (_tweenerOpen.IsPlaying())
        {
            yield return null;
        }
        Debug.Log("after open over");
    }

    private IEnumerator beforeClose()
    {
        Debug.Log("before close start");
        Tween _tweenerClose = transform.DOScale(Vector3.zero, 0.7f);
        yield return new WaitForEndOfFrame();
        while (_tweenerClose.IsPlaying())
        {
            yield return null;
        }
        Debug.Log("before close over");
    }

}
