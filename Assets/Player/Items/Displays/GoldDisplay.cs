using Player.Items;
using TMPro;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{
 public TextMeshProUGUI text;
 private int count;
 
 private void OnEnable()
 {
  Gold.OnGoldCollected += IncrementGoldCount;
 }

 private void OnDisable()
 {
  Gold.OnGoldCollected -= IncrementGoldCount;
 }

 public void IncrementGoldCount()
 {
  count++;
  text.text = $"{count}";
}
}
