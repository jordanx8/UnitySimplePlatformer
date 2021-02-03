using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public bullet bulletPrefab;
    public bullet_left bulletPrefab2;
    public Transform LaunchOffset;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;
        Vector3 mousePosition = Input.mousePosition;
        if(Input.GetKey(KeyCode.W))
        {
            if(facingRight)
            {
                direction = new Vector2(-87,181);
            }
            else
            {
                direction = new Vector2(282, 712);
            }
            transform.up = direction;
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            direction = new Vector2(0,0);
            transform.up = direction;
        }

        if(Input.GetKey(KeyCode.D))
        {
            if(! facingRight)
            {
                Flip();
            }
        }
        if(Input.GetKey(KeyCode.A))
        {
            if(facingRight)
            {
                Flip();
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(facingRight)
            {
                Instantiate(bulletPrefab, LaunchOffset.position, transform.rotation);
            }
            else
            {
                Instantiate(bulletPrefab2, LaunchOffset.position, transform.rotation);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
    }

}
