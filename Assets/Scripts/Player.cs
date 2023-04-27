using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    List<Enemy> availableEnemies = new List<Enemy>();

    //If enemy enters the attack range, the enemy script is added to a list
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Entered!");
            availableEnemies.Add(col.GetComponent<Enemy>());
        }
    }

    //If enemy exits the attack range, the enemy script is removed from the list
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited");
        if (other.gameObject.tag == "Enemy")
        {
            availableEnemies.Remove(other.GetComponent<Enemy>());
        }
    }
    void Start()
    {
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    //Iterates through list of enemies and deals damge to each.
    void Attack()
    {
        Enemy theEnemy;
        for (int i = 0; availableEnemies.Count > i; i++)
        {
            Debug.Log("Attacking " + i);
            theEnemy = availableEnemies[i];
            theEnemy.TakeDamage(3);
        }
    }
}
