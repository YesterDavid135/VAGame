using Player.Items;
using TMPro;
using UnityEngine;

public class CopperDisplay : MonoBehaviour
{
 public TextMeshProUGUI text;
 private int count;
 
 private void OnEnable()
 {
  Copper.OnCopperCollected += IncrementCopperCount;
 }

 private void OnDisable()
 {
  Copper.OnCopperCollected -= IncrementCopperCount;
 }

 public void IncrementCopperCount()
 {
  count++;
  text.text = $"{count}";
}
}
