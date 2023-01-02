using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RebornPopUp : PopUp
{
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Configurations()
    {
        AfterOpen = AfterOpen_;
        BeforeOpen = BeforeOpen_;

        //BeforeClose = BeforeClose_;
    }

    private IEnumerator BeforeOpen_()
    {
        //Debug.Log("before open start");
        transform.localScale = Vector3.zero;
        yield return null;
        //Debug.Log("before open over");
    }

    private IEnumerator AfterOpen_()
    {
        //Debug.Log("after open start");
        Tween _tweenerOpen = transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        yield return new WaitForEndOfFrame();
        while (_tweenerOpen.IsPlaying())
        {
            yield return null;
        }
        //Debug.Log("after open over");
    }

    private IEnumerator BeforeClose_()
    {
        //Debug.Log("before close start");
        Tween _tweenerClose = transform.DOScale(Vector3.zero, 0.3f);
        yield return new WaitForEndOfFrame();
        while (_tweenerClose.IsPlaying())
        {
            yield return null;
        }
        //Debug.Log("before close over");
    }


}