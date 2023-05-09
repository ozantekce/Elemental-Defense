using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScreenManagerNS
{
    public interface IScreenElement
    {

        public IScreenStatus Status { get; set; }
        public MonoBehaviour MonoBehaviour { get; set; }

        #region Events
        public UnityEvent BeforeOpen { get; set; }
        public UnityEvent AfterOpen { get; set; }

        public UnityEvent BeforeClose { get; set; }
        public UnityEvent AfterClose { get; set; }
        #endregion

        public ScreenManagerAnimation OpenAnimation { get; set; }
        public ScreenManagerAnimation CloseAnimation { get; set; }



        public void Configurations();


        public void Open()
        {
            ScreenManager.Instance.StartCoroutine(OpenRoutine());
        }

        private IEnumerator OpenRoutine()
        {
            // must be closed
            if (Status != IScreenStatus.Closed) yield break;

            Status = IScreenStatus.Opening;
            if (BeforeOpen != null) BeforeOpen.Invoke();
            if (OpenAnimation != null) yield return OpenAnimation.Enumerator(this);

            if (Status != IScreenStatus.Opening) yield break;
            OpenNow();

            if (AfterOpen != null) AfterOpen.Invoke();
        }

        private void OpenNow()
        {
            //Debug.Log("OPEN");
            Status = IScreenStatus.Opened;
            MonoBehaviour.gameObject.SetActive(true);
            MonoBehaviour.transform.SetAsLastSibling();
        }

        public void Close()
        {
            ScreenManager.Instance.StartCoroutine(CloseRoutine());
        }

        private IEnumerator CloseRoutine()
        {
            // must be opened
            if (Status != IScreenStatus.Opened) yield break;

            Status = IScreenStatus.Closing;
            if (BeforeClose != null) BeforeClose.Invoke();
            if (CloseAnimation != null) yield return CloseAnimation.Enumerator(this);

            if (Status != IScreenStatus.Closing) yield break;
            CloseNow();

            if (AfterClose != null) AfterClose.Invoke();
        }

        private void CloseNow()
        {
            //Debug.Log("CLOSE");
            Status = IScreenStatus.Closed;
            MonoBehaviour.gameObject.SetActive(false);
        }

        public enum IScreenStatus { Closed, Closing, Opened, Opening }

    }

}