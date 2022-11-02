using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    float jumPower = 10;
    Rigidbody2D rb;
    bool canjump = false;
    public float maxSpeed = 20f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {


        GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxSpeed);

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("Walking", true);
        }


        if (Input.GetButtonDown("Jump") && canjump == true)
        {
            rb.AddForce(Vector2.up * jumPower, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Evil"))
        {
            transform.Translate(11f, 0f, 1f);
        }


        if (collision.gameObject.CompareTag("Ground"))
        {
            canjump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke("SetCanJump", 0.2f);
        }
    }
    private void SetCanJump()
    {
        canjump = false;
    }
}
