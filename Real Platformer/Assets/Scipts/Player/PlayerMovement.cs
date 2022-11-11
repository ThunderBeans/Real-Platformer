using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public Vector3 StartPosition;
    private Vector3 scaleChange;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        StartPosition = gameObject.transform.position;

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
            transform.position = StartPosition;
        }

        if (collision.gameObject.CompareTag("Flag"))
        {
            anim.SetBool("Campfire", true);
            Invoke("scaleChanger", 0.5f);
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
            Invoke("SetCanJump",0);
        }
    }
    private void SetCanJump()
    {
        canjump = false;
    }
    private void scaleChanger()
    {
        scaleChange = new Vector3(1.3f, 1.3f, 1.0f);
        transform.localScale = scaleChange;
    }
}
