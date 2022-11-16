using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

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
    bool finish = false;
    public Animator transition;
    public float transitionTime = 1f;

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

        if (finish == false) ;
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

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
            Invoke("LoadNextLevel",7f);
            anim.SetBool("Campfire", true);
            Invoke("scaleChanger", 0.2f);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            finish = true;
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
            Invoke("SetCanJump", 0.15f);
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
        maxSpeed = 2f;
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
