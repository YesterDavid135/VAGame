using Player.Items;
using TMPro;
using UnityEngine;

public class ElectronicDisplay : MonoBehaviour
{
 public TextMeshProUGUI text;
 private int count;
 
 private void OnEnable()
 {
  Electronics.OnElectronicCollected += IncrementElectronicCount;
 }

 private void OnDisable()
 {
  Electronics.OnElectronicCollected -= IncrementElectronicCount;
 }

 public void IncrementElectronicCount()
 {
  count++;
  text.text = $"{count}";
}
}
