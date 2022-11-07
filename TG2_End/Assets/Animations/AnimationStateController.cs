using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool("isRunning");
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        } 

        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool("isRunning", true);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool("isRunning", false);
        }
    }
}
