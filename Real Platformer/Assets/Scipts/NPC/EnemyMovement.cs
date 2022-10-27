using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    float jumPower = 10;
    Rigidbody2D rb;
    bool canjump = false;
    int direction = 0;// 0 recht 1 links


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else if (direction == 1)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }


        if (false && canjump == true)
        {
            rb.AddForce(Vector2.up * jumPower, ForceMode2D.Impulse);
        }

        if (direction) //ik wil hier code dat de enemy van kant wisselt as het x afstand weg is van de player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canjump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke("SetCanJump", 0.05f);
        }
    }
    private void SetCanJump()
    {
        canjump = false;
    }
}
