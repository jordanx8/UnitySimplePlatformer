using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Don_basiccontrols : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public healthBar healthBar;
    public float speed;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sr;
    private bool anim_up = false;
    private bool anim_walking = false;
    private bool facingRight = true;
    private bool onGround = false;
    private bool isFalling = false;
    private bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Physics-------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------

        //check if Don is on the ground
        //as of now, as long as Don is touching some kind of block; he is "onGround"
        if(rb2d.IsTouchingLayers(Physics2D.AllLayers))
        {
            isJumping = false;
            onGround = true;
            //checks if Don is landing
            if(isFalling)
            {
                isFalling = false;
                anim.SetBool("isFalling", isFalling);
            }
        }
        else
        {
            onGround = false;
        }

        //checks if Don starts to fall
        if (rb2d.velocity.y < -0.1)
        {
            isFalling = true;
            anim.SetBool("jump", false);
            anim.SetBool("isFalling", isFalling);
        }


        //Controls------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------
        
        //Up arrow control; allows Don to look up
        if(Input.GetKey(KeyCode.W))
        {
            anim_up = true;
            anim.SetBool("up", anim_up);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            anim_up = false;
            anim.SetBool("up", anim_up);
        }

        //Right arrow control; allows Don to move right while key is held down
        if(Input.GetKey(KeyCode.D))
        {
            anim_walking = true;
            if(! facingRight)
            {
                Flip();
            }
            anim.SetBool("walk", anim_walking);
            rb2d.AddForce(new Vector2(speed,0f));
        }
        //stops movement when key is let go
        if(Input.GetKeyUp(KeyCode.D))
        {
            anim_walking = false;
            anim.SetBool("walk", anim_walking);
        }

        //Left arrow control; allows Don to move right while key is held down
        if(Input.GetKey(KeyCode.A))
        {
            anim_walking = true;
            if(facingRight)
            {
                Flip();
            }
            anim.SetBool("walk", anim_walking);
            rb2d.AddForce(new Vector2(-speed,0f));
        }
        //stops movement when key is let go
        if(Input.GetKeyUp(KeyCode.A))
        {
            anim_walking = false;
            anim.SetBool("walk", anim_walking);
        }



        //Space bar control; allows Don to jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //check if Don has already jumped or not
            if(onGround)
            {
                isJumping = true;
                anim.SetBool("jump", isJumping);
                rb2d.AddForce(transform.up * 3f, ForceMode2D.Impulse);
            }
        }
        if(currentHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "enemy")
        {
            score.scoreValue -= 10;
            sr.color = new Color(2,0,0);
            rb2d.AddForce(transform.up * 2, ForceMode2D.Impulse);
            currentHealth = currentHealth - 10;
            healthBar.SetHealth(currentHealth);
        }
        if(col.gameObject.tag != "enemy")
        {
            sr.color = Color.white;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
