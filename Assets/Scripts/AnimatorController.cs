using System;
using System.ComponentModel;
using UnityEngine;

/// AnimatorController invokes an event after animation finished
public class AnimatorController : MonoBehaviour
{
    public event EventHandler OnAnimationHasFinished;

    public void hasFinishedAnimation() {
        OnAnimationHasFinished?.Invoke(this, new EventArgs());
    }
}
