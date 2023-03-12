using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ScreenManagerNameSpace
{
    public class PopUp : MonoBehaviour, IScreenElement, IPointerDownHandler
    {

        public bool Opened { get; set; }

        public MonoBehaviour MonoBehaviour { get => this; set { } }
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }

        private IScreenElement.Method _beforeOpen;
        private IScreenElement.Method _afterOpen;

        private IScreenElement.Method _beforeClose;
        private IScreenElement.Method _afterClose;

        public IScreenElement.Method BeforeOpen { get => _beforeOpen; set => _beforeOpen = value; }
        public IScreenElement.Method AfterOpen { get => _afterOpen; set => _afterOpen = value; }
        public IScreenElement.Method BeforeClose { get => _beforeClose; set => _beforeClose = value; }
        public IScreenElement.Method AfterClose { get => _afterClose; set => _afterClose = value; }


        public virtual void Configurations()
        {

        }

    }

}
