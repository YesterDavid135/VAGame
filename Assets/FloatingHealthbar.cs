using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 Offset;

    
    public void UpdateHealthBar(float currentValue,float maxValue)
    {
        slider.value = currentValue / maxValue;
        if (text != null)
        {
            text.text = "Health: " + Math.Round(currentValue,2) + " / " + maxValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.rotation = camera.transform.rotation;
            transform.position = target.position + Offset;
        }
    }
}
