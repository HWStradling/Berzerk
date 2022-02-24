using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // damage enemy
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        /*if (collision.tag == "Player")
        {
            // damage enemy
        }*/ // use if implement ricochets;
    }
}