using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimControl : MonoBehaviour
{
    #region Singleton
    public static PlayerAnimControl instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    Animator animator;
    PlayerAnimStates currentState;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SetAnimState (PlayerAnimStates state)
    {
        string animName = Enum.GetName(typeof(PlayerAnimStates), state);
        currentState = state;
        if (animator !=null)
        {
            animator.Play(animName);
        }
    }

   
}
