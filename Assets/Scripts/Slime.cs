using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] float movespeed;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Slime HP = " + life);
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = movespeed;
        var localScale = transform.localScale;
        if (movespeed > 0)
        {
            localScale.x = 1f;
        }
        else
        {
            localScale.x = -1f;
        }
        transform.localScale = localScale;
    }

    public void Damage(int damage)
    {
        Debug.Log("Damage received: " + damage);

        life -= damage;

        if (life <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
