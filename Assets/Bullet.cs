using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                Destroy(gameObject);
                break;
        }
    }
}