using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenManagerNS
{
    public abstract class ScreenManagerAnimation : ScriptableObject
    {
        public abstract IEnumerator Enumerator(IScreenElement screenElement);

    }

}
