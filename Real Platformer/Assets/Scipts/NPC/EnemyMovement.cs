using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5;
    Rigidbody2D rb;
    int direction = 0;// 0 rechts 1 links
    [SerializeField]
    float distance = 1.4f;

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

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (direction == 0 && collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(direction);
            direction = 1;
        }
        // 0 is links 1 is rechts
        if (direction == 1 && collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(direction);
            Invoke("dir_swap", distance);
        }
    }


    private void dir_swap()
        {
             direction = direction - direction;
        }
}
