using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ScreenManagerNS
{
    [CreateAssetMenu(fileName = "OutInAnimation", menuName = "ScreenManagerAnimation/OutInAnimation")]
    public class OutInAnimation : ScreenManagerAnimation
    {
        private bool firstTime = true;
        private Vector3 startSize;
        public Vector3 endSize;
        public float duration;


        private WaitForEndOfFrame waitForEndOfFrame;
        public override IEnumerator Enumerator(IScreenElement screenElement)
        {
            if (firstTime)
            {
                firstTime = false;
                startSize = screenElement.MonoBehaviour.transform.localScale;
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
            screenElement.MonoBehaviour.transform.localScale = startSize;
        }
    }

}