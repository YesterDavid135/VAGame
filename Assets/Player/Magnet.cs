using System;
using System.Collections;
using System.Collections.Generic;
using Player.Items;
using UnityEngine;

public class Magnet : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Copper") ||
            collision.gameObject.CompareTag("Gold") ||
            collision.gameObject.CompareTag("Steel") ||
            collision.gameObject.CompareTag("Electronic")) {
            ICollectible item = collision.gameObject.GetComponent<ICollectible>();
            if (item != null) {
                item.SetTarget(transform.parent.position);
            }
        }
    }
}