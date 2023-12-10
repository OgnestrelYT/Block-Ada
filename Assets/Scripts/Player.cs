using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 3f;
    public float fast_speed = 1f;
    public float jumpForce = 5f;

    public bool flip = false;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");

        if (movement == 0)
        {
            animator.SetInteger("Speed", 0);
        }
        else
        {
            animator.SetInteger("Speed", 1);
        }

        transform.position += new Vector3(movement, 0, 0) * speed * fast_speed * Time.deltaTime;

        //if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.05f){
        //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //}

        if (Input.GetKey(KeyCode.LeftShift)){
            if (movement != 0)
            {
                fast_speed = 2f;
                animator.SetBool("IsRun", true);
            }
            else
            {
                animator.SetBool("IsRun", false);
            }
        } else {
            fast_speed = 1f;
            animator.SetBool("IsRun", false);
        }

        if (movement < 0){
            flip = true;
        } else {
            if (movement > 0){
                flip = false;
            }
        }
        sr.flipX = flip;
    }
}
