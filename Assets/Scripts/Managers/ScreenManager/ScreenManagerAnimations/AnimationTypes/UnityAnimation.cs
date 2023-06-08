using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ScreenManagerNS
{
    [CreateAssetMenu(fileName = "UnityAnimation", menuName = "ScreenManagerAnimation/UnityAnimation")]

    public class UnityAnimation : ScreenManagerAnimation
    {

        [SerializeField]
        private AnimationClip clip;

        private static Dictionary<IScreenElement, Animation> keyValuePairs;

        private Animation animation;

        private WaitForEndOfFrame waitForEndOfFrame;
        public override IEnumerator Enumerator(IScreenElement screenElement)
        {

            if (clip == null) yield break;

            clip.legacy = true;
            
            if (keyValuePairs == null)
            {
                keyValuePairs = new Dictionary<IScreenElement, Animation>();
            }
            if (!keyValuePairs.ContainsKey(screenElement))
            {
                Animation animation = screenElement.MonoBehaviour.gameObject.GetComponent<Animation>();
                if (animation == null)
                    animation = screenElement.MonoBehaviour.gameObject.AddComponent<Animation>();
                keyValuePairs.Add(screenElement, animation);
            }

            animation = keyValuePairs[screenElement];
            if (animation.GetClip(clip.name) == null)
            {
                animation.AddClip(clip, clip.name);
            }
            screenElement.MonoBehaviour.gameObject.SetActive(true);
            animation.Play(clip.name);

            yield return waitForEndOfFrame;

            animation.Play(clip.name);
            while (animation.IsPlaying(clip.name))
            {
                if (screenElement.Status == IScreenElement.IScreenStatus.Opened
                    || screenElement.Status == IScreenElement.IScreenStatus.Closed
                    )
                {
                    animation.Stop();
                    yield break;
                }
                yield return waitForEndOfFrame;
            }

        }

    }
}