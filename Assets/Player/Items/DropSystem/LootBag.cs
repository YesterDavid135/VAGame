using System.Collections;
using System.Collections.Generic;
using Player.Items;
using UnityEngine;

public class LootBag : MonoBehaviour
{
 public List<GameObject> lootPrefabs = new List<GameObject>();

 List<GameObject> GetDroppedItems()
 {
  int randomNumber = Random.Range(1, 101);
  List<GameObject> possibleItems = new List<GameObject>();
  foreach (GameObject lootItem in lootPrefabs)
  {
   if (randomNumber <= lootItem.GetComponent<ICollectible>().getDropChance())
   {
    possibleItems.Add(lootItem);
   }
  }

  if (possibleItems.Count > 0)
  {
   return possibleItems;
  }

  return null;
 }

 public void InstantiateLoot(Vector3 spawnPosition)
 {
  List<GameObject> droppedItems = GetDroppedItems();
  if (droppedItems != null)
  {
   foreach (GameObject item in droppedItems)
   {
    GameObject lootGameObject = Instantiate(item, spawnPosition, Quaternion.identity);
    
    float dropForce = 350f;
    Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce);
   }
  }
 }
}
