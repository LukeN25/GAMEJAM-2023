using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject enemy;

    void Start()
    {
        
    }

    void Update()
    {

    }

    //destroys enemy, probably edit this later
    public void Die()
    {
        Destroy(enemy);
    }

    //play this to make enemy take damage
    public void TakeDamage(int damage)
    {
        //remove health from the enemy and if it has less than 0 it dies
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
