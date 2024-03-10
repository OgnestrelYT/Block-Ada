using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 3f;
    public float fast_speed = 1f;
    public float jumpForce = 5f;

    [HideInInspector, SerializeField] public bool flip = false;

    public float decreaseStamina = 0.1f;
    public float increaseStamina = 0.1f;
    [HideInInspector, SerializeField] public static bool isStop = false;
    [HideInInspector, SerializeField] public bool canRun = true;
    [HideInInspector, SerializeField] public static bool canMove = true;
    [HideInInspector, SerializeField] public static int stam = 200;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Scenes.need) {
            if (other.tag == Scenes.objtag) {
                Scenes.canSkipT = true;
            }
        }
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

        if (canMove) {
            transform.position += new Vector3(movement, 0, 0) * speed * fast_speed * Time.deltaTime;
        } else {
            animator.SetInteger("Speed", 0);
        }
        

        //if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.05f){
        //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //}

        if ((Input.GetKey(KeyCode.LeftShift)) && (canMove) && !(PauseMenu.PauseGame)) {
            if (StaminaBar.staminaNow > 0){
                if ((movement != 0) && (canRun))
                {
                    StaminaBar.staminaNow -= decreaseStamina * Time.deltaTime * stam;
                    fast_speed = 2f;
                    animator.SetBool("IsRun", true);
                }
                else
                {
                    // if ((StaminaBar.staminaNow < StaminaBar.staminaMax) && (!isStop)) {
                    //     StaminaBar.staminaNow += increaseStamina * Time.deltaTime * stam;
                    // }
                    animator.SetBool("IsRun", false);
                }
            }
            else
            {
                StaminaBar.staminaNow = 0;
                if ((StaminaBar.staminaNow < StaminaBar.staminaMax) && (!isStop)) {
                    StaminaBar.staminaNow += increaseStamina * Time.deltaTime * stam;
                    canRun = false;
                }
                fast_speed = 1f;
                if (!(PauseMenu.PauseGame)) {
                    animator.SetBool("IsRun", false);
                }
            }
            
        } else {
            canRun = true;
            if ((StaminaBar.staminaNow < StaminaBar.staminaMax) && (!isStop) && !(PauseMenu.PauseGame)){
                StaminaBar.staminaNow += increaseStamina * Time.deltaTime * stam;
            }

            fast_speed = 1f;
            if (!(PauseMenu.PauseGame)) {
                animator.SetBool("IsRun", false);
            }
        }

        if (movement < 0){
            flip = true;
        } else {
            if (movement > 0){
                flip = false;
            }
        }
        if (canMove) {
            sr.flipX = flip;
        }
    }
}
