using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private ParticleSystem psystem;
    public float health;
    public float attackDamage;
    public float moveSpeed;
    private bool direction = false;
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        psystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if(! direction)
            {
                rb2d.AddForce(new Vector2(-moveSpeed,0f));
            }
            else
            {
                rb2d.AddForce(new Vector2(moveSpeed,0f));
            }
            if(health == 0)
            {
                score.scoreValue += 50;
                Destroy(gameObject);
            }
        }
    }

    void OnBecameVisible()
    {
        start = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "bullet")
        {
            score.scoreValue += 10;
            Destroy(col.gameObject);
            health--;
            psystem.Play();
        }
        if(col.gameObject.tag == "level")
        {
            direction = !direction;
        }
        if(col.gameObject.tag == "Player")
        {
            direction = !direction;
        }
    }

}
