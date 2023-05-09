using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScreenManagerNS
{
    [CreateAssetMenu(fileName = "InOutAnimation", menuName = "ScreenManagerAnimation/InOutAnimation")]
    public class InOutAnimation : ScreenManagerAnimation
    {
        private bool firstTime = true;
        public Vector3 startSize;
        private Vector3 endSize;
        public float duration;


        private WaitForEndOfFrame waitForEndOfFrame;
        public override IEnumerator Enumerator(IScreenElement screenElement)
        {
            if (firstTime)
            {
                firstTime = false;
                endSize = screenElement.MonoBehaviour.transform.localScale;
            }
            screenElement.MonoBehaviour.transform.localScale = startSize;
            screenElement.MonoBehaviour.gameObject.SetActive(true);
            Tween _tweenerOpen = screenElement.MonoBehaviour
                .transform.DOScale(endSize, duration);
            if (waitForEndOfFrame == null) waitForEndOfFrame = new WaitForEndOfFrame();
            yield return waitForEndOfFrame;
            while (_tweenerOpen.IsPlaying())
            {

                if (screenElement.Status == IScreenElement.IScreenStatus.Opened
                    || screenElement.Status == IScreenElement.IScreenStatus.Closed
                    )
                {
                    _tweenerOpen.Kill();
                    yield break;
                }

                yield return waitForEndOfFrame;
            }
        }
    }
}
