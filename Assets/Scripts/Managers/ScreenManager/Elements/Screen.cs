using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScreenManagerNS
{
    public class Screen : MonoBehaviour, IScreenElement
    {

        public IScreenElement.IScreenStatus Status { get; set; }

        public MonoBehaviour MonoBehaviour { get => this; set { } }

        [SerializeField]
        private UnityEvent _beforeOpen;
        [SerializeField]
        private UnityEvent _afterOpen;
        [SerializeField]
        private UnityEvent _beforeClose;
        [SerializeField]
        private UnityEvent _afterClose;
        [SerializeField]
        private ScreenManagerAnimation _openAnimation;
        [SerializeField]
        private ScreenManagerAnimation _closeAnimation;


        public virtual void Configurations()
        {

        }

        #region GetterSetter
        public UnityEvent BeforeOpen { get { return _beforeOpen; } set { _beforeOpen = value; } }
        public UnityEvent AfterOpen { get { return _afterOpen; } set { _afterOpen = value; } }
        public UnityEvent BeforeClose { get { return _beforeClose; } set { _beforeClose = value; } }
        public UnityEvent AfterClose { get { return _afterClose; } set { _afterClose = value; } }
        public ScreenManagerAnimation OpenAnimation { get { return _openAnimation; } set { _openAnimation = value; } }
        public ScreenManagerAnimation CloseAnimation { get { return _closeAnimation; } set { _closeAnimation = value; } }

        #endregion

    }
}
