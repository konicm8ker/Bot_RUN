using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationManager : MonoBehaviour
{
    public bool animationPlaying = false;

    public bool CheckAnimationState()
    {
        return animationPlaying;
    }

    public void AnimationFinished()
    {
        animationPlaying = false;
    }
}
