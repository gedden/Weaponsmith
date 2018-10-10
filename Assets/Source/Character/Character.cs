using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    public enum ECharacterAnimation
    {
        Idle,
        Walk,
        Smith,
        Cast,
    }


    // Member Variables


    // Private Members
    private Animator Anim;
    private ECharacterAnimation? CurrentAnimation = null;

    // Use this for initialization
    void Start ()
    {
        Anim = GetComponent<Animator>();
	}

    public ECharacterAnimation Animation
    {
        get
        {
            if (CurrentAnimation == null)
                return ECharacterAnimation.Idle;

            return (ECharacterAnimation)CurrentAnimation;
        }
        set
        {
            if (value == CurrentAnimation)
                return;

            // Unflag the old state
            if (CurrentAnimation != null)
                SetAnimationFlag((ECharacterAnimation)CurrentAnimation, false);

            // Set the new flag
            CurrentAnimation = value;
            SetAnimationFlag(value, true);
        }
    }

    /// <summary>
    /// Set the internal animation flag state
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="bEnabled"></param>
    private void SetAnimationFlag(ECharacterAnimation Key, bool bEnabled)
    {
        switch (Key)
        {
            case ECharacterAnimation.Idle:
                Anim.SetBool("Idle", bEnabled);
                break;
            case ECharacterAnimation.Walk:
                Anim.SetBool("Walk", bEnabled);
                break;
            case ECharacterAnimation.Smith:
                Anim.SetBool("Smith", bEnabled);
                break;
            case ECharacterAnimation.Cast:
                Anim.SetBool("Cast", bEnabled);
                break;
        }
    }

    private void ResetAnimation()
    {
        // Get the controller
        Anim.SetBool("Walk", CurrentAnimation == ECharacterAnimation.Walk);
        Anim.SetBool("Smith", CurrentAnimation == ECharacterAnimation.Smith);
        Anim.SetBool("Cast", CurrentAnimation == ECharacterAnimation.Cast);
        Anim.SetBool("Idle", CurrentAnimation == ECharacterAnimation.Idle);
    }

    private float last = 0;
	// Update is called once per frame
	void Update ()
    {
        var now = Time.time;

        if (last + 5 < now)
        {
            Debug.Log("Update!");
            switch (Animation)
            {
                case ECharacterAnimation.Idle:
                    Animation = ECharacterAnimation.Walk;
                    break;
                case ECharacterAnimation.Walk:
                    Animation = ECharacterAnimation.Smith;
                    break;
                case ECharacterAnimation.Smith:
                    Animation = ECharacterAnimation.Cast;
                    break;
                case ECharacterAnimation.Cast:
                    Animation = ECharacterAnimation.Idle;
                    break;
            }
            last = now;
        }
	}
}
