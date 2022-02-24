using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // note: bullet clips players collider on instantiation, fix before implementing ricochets,
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
