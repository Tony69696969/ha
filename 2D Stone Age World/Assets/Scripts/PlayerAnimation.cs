using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    PlayerController playerController;
    float lastAttackTime=-10;
    public float attackCoolDownTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("run", Mathf.Abs(playerController.horizontalMove));
        anim.SetFloat("verticalVelocity", playerController.rb.velocity.y / 10);
        if (playerController.isOnGround)
        {
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(Time.time >= (lastAttackTime+attackCoolDownTime))
            {
                Attack();
            }
        }
    }
    void Attack()
    {
        lastAttackTime = Time.time;
        anim.SetTrigger("attack");
    }
}

//15168