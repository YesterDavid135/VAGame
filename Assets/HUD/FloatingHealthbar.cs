using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 Offset;
    
    public void UpdateHealthBar(float currentValue,float maxValue) {
        slider.value = currentValue / maxValue;
        if (text != null) {
            text.text = "Health: " + Math.Round(currentValue,2) + " / " + maxValue;
        }
    }
    void Update() {
        if (target != null) {
            transform.rotation = camera.transform.rotation;
            transform.position = target.position + Offset;
        }
    }
}
