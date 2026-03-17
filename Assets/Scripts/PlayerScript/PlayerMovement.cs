using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public RectTransform JoyStickHandleTransaform;
    private SpriteRenderer rd;
    private Animator Anim;

    public enum MovementState{idle, walking};
    void Start()
    {
        Anim = GetComponent<Animator>();
        rd = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //IDLE and WALKING
        AnimationStateUpdate();

    }


    //IDLE and WALKING
    public void AnimationStateUpdate()
    {
        
        MovementState state;
        if (JoyStickHandleTransaform.transform.position.x < 248)
        {
            state = MovementState.walking;
            rd.flipX = false;
        }
        else if(JoyStickHandleTransaform.position.x > 248)
        {
            state = MovementState.walking;
            rd.flipX = true;
        }
        else if(JoyStickHandleTransaform.transform.position.x == 248)
        {
            state = MovementState.idle;
            rd.flipX = false;
        }
        else
        {
            state = MovementState.idle;
            rd.flipX = false;
        }
        Anim.SetInteger("state", (int)state);
    }

}
