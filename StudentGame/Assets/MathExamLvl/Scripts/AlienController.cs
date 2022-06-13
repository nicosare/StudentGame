using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public Animator animator;

    public void PlayAnimation()
    {
        animator.SetBool("isStartAnimation", true);
    }
}
