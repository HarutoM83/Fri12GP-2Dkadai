using UnityEngine;

public class Sword : MonoBehaviour
{
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger hit: " + other.name);
        if (!player.IsAttacking()) return;

        Slime slime = other.GetComponent<Slime>();

        if (slime != null)
        {
            slime.Damage(player.Attackpower);
        }
    }
}
