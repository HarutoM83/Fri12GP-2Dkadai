using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] float movespeed;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
