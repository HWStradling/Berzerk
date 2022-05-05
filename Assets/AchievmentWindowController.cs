using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentWindowController : MonoBehaviour
{
    [SerializeField]private Animator animator;
    private void Start()
    {
        
    }
    public void FadeIn()
    {
        animator.Play("fade_in");
    }
    public void FadeOut()
    {
        animator.Play("fade_out");
    }
}
