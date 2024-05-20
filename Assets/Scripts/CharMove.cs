using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public GameObject player;
    public GameObject cam;
    public Animator animator;
    public Rigidbody2D rb;
    public Collider2D trig;
    float moveX;
    float camPosX;
    float camPosY;
    public float speed;
    public float jumpSpeed;
    public bool isShooting;
    float runSpeed = 0;
    bool facingRight = true;
    public bool isGrounded = false;
    public bool isRunning;
    public int health;

    void Start()
    {
        health = 5;
    }


    void Update()
    {
        camControl();
        moveX = player.transform.position.x + (Input.GetAxis("Horizontal") * (speed + runSpeed) * Time.deltaTime);

        if (isShooting == false)
        {
            move();
        }
        jump();


    }


    public void getDamage()
    {
        health = Mathf.Clamp(health, 0, 5);
        health -= 1;
    }

    



    void camControl()
    {

        camPosX = player.transform.position.x;
        camPosY = player.transform.position.y;
        cam.transform.position = new Vector3(camPosX, camPosY, -10.0f);



    }

    void jump()
    {

        if (isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("w basýldý");
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);

            }
        }

    }

    private void move()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {


            animator.SetBool("isWalking", true);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                isRunning = true;
                runSpeed = 3;


            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", false);
                isRunning = false;
                runSpeed = 0;
            }

            player.transform.position = new Vector3(moveX, player.transform.position.y, 2.0f);
            facing();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void facing()
    {
        if (Input.GetAxis("Horizontal") < 0 && facingRight == true)
        {

            float a = player.transform.localScale.x;
            a = a * -1;
            player.transform.localScale = new Vector3(a, player.transform.localScale.y, player.transform.localScale.y);

            facingRight = false;

        }
        else if (Input.GetAxis("Horizontal") > 0 && facingRight == false)
        {
            float a = player.transform.localScale.x;
            a = a * -1;
            player.transform.localScale = new Vector3(a, player.transform.localScale.y, player.transform.localScale.y);

            facingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

}

