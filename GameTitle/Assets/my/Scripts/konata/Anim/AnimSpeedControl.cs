using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モーションの制御
/// </summary>
public class AnimSpeedControl : MonoBehaviour
{
    public Animator animator;
    public int tempo = 120;

    float multiple;
    float barTime;
    int count;

    public static bool SetOnAttack { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //テンポによって再生速度が変わる
        barTime = 60 * 4 * 1 / tempo;
        multiple = 1 / barTime;
        animator.SetFloat("Speed", multiple * 2);
    }

    // Update is called once per frame
    void Update()
    {

        //モーションの制御
        if (SetOnAttack)
        {
            //腕振り上げモーション
            if (count % 2 == 0)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Default", false);
                animator.SetBool("Left", true);
            }
            else
            {
                animator.SetBool("Left", false);
                animator.SetBool("Default", false);
                animator.SetBool("Right", true);
            }
            count++;
            SetOnAttack = false;
        }
        else
        {
            //待機モーション
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            animator.SetBool("Default", true);
            
        }
    }
}
