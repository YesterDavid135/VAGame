using Player.Items;
using TMPro;
using UnityEngine;

public class SteelDisplay : MonoBehaviour
{
 public TextMeshProUGUI text;
 private int count;
 
 private void OnEnable()
 {
  Steel.OnSteelCollected += IncrementSteelCount;
 }

 private void OnDisable()
 {
  Steel.OnSteelCollected -= IncrementSteelCount;
 }

 public void IncrementSteelCount()
 {
  count++;
  text.text = $"{count}";
}
}
