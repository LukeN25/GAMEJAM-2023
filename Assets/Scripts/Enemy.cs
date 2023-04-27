using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject enemy;
    public float knockBack = 200f;
    public int damage = 3;

    void Start()
    {
        
    }

    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D other)
 {
    //I have no idea how this works
    if (other.gameObject.tag == "Player")
    {
        var force = transform.position - other.transform.position;
        force.Normalize ();
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(-force * knockBack);
        other.gameObject.GetComponent<Player>().TakeDamage(damage);
    }
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
