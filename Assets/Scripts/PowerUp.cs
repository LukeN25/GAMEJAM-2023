using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject pickupEffect;
    public float multiplier = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
         if(other.CompareTag("Player"))
         {
              Pickup(other);
         }
    }

    void Pickup(Collider2D player)
    {
       //Effect spawn
       Instantiate(pickupEffect, transform.position, transform.rotation);
       

       //Player Upgrade
       Player stats = player.GetComponent<Player>();
       stats.jumpingPower *= multiplier;

       //Power-Up Disappear
       Destroy(gameObject);
    }
}