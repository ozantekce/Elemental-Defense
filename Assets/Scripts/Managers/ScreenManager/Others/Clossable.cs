using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace ScreenManagerNS
{
    public class Clossable : MonoBehaviour
    {


        private GameObject _upFrame;

        [SerializeField]
        private int _size = 6;

        [SerializeField]
        private Sprite _closeSprite;

        private RectTransform _rectTransform;

        void Start()
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
                Debug.Log("Closeable");
                ScreenManager.Instance.ClosePopUp(gameObject.name);

            });
            eventTriggerClose.triggers.Add(pointerDownClose);

            closeSymbol.transform.localScale = Vector3.one;
        }



    }

}