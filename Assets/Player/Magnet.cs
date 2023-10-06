using System;
using System.Collections;
using System.Collections.Generic;
using Player.Items;
using UnityEngine;

public class Magnet : MonoBehaviour
{
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.gameObject.TryGetComponent<ICollectible>(out ICollectible item))
    {
      item.SetTarget(transform.parent.position);
    }
  }
}
