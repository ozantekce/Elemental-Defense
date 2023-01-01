using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovableUIElement : MonoBehaviour
{

    private GameObject _upFrame;
    private bool _pressed;

    private RectTransform _rectTransform;

    [SerializeField]
    private int _size = 6;

    [SerializeField]
    private bool _addCloseSymbol;
    [SerializeField]
    private Sprite _closeSprite;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        GameObject upFrame = new GameObject("UpFrame");

        upFrame.transform.SetParent(transform);
        RectTransform rectTransform = upFrame.AddComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(1, 1);

        rectTransform.sizeDelta = new Vector2(0, _size * 2);
        rectTransform.anchoredPosition = new Vector2(0, -_size);

        upFrame.AddComponent<Image>();



        EventTrigger eventTrigger = upFrame.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener(delegate { _pressed = true; transform.SetAsLastSibling(); });
        eventTrigger.triggers.Add(pointerDown);

        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener(delegate { _pressed = false; });
        eventTrigger.triggers.Add(pointerUp);

        _upFrame = upFrame;

        _upFrame.transform.localScale = Vector3.one;


        if (_addCloseSymbol)
        {
            GameObject closeSymbol = new GameObject("CloseSymbol");
            Image image = closeSymbol.AddComponent<Image>();
            image.transform.SetParent(upFrame.transform);
            image.rectTransform.anchorMin = new Vector2(1, 0.5f);
            image.rectTransform.anchorMax = new Vector2(1, 0.5f);
            image.rectTransform.anchoredPosition = new Vector2(-_size, 0);
            image.rectTransform.sizeDelta = new Vector2(_size * 2, _size * 2);
            image.sprite = _closeSprite;

            EventTrigger eventTriggerClose = image.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerDownClose = new EventTrigger.Entry();
            pointerDownClose.eventID = EventTriggerType.PointerDown;
            pointerDownClose.callback.AddListener(delegate {

                ScreenManager.Instance.ClosePopUp(gameObject.name);
            
            });
            eventTriggerClose.triggers.Add(pointerDownClose);

            closeSymbol.transform.localScale = Vector3.one;
        }




    }



    private Vector2 _mouseLastPos;


    private void Update()
    {

        Vector2 mousePos = Input.mousePosition;

        if (_pressed)
        {
            Vector2 mouseDelta = mousePos - _mouseLastPos;
            Vector2 pos = transform.position;
            pos += mouseDelta;
            transform.position = pos;

        }


        _mouseLastPos = mousePos;

    }




}
