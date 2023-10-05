using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class XPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI lvlAnzeige;
    
    public void UpdateXPBar(float currentValue,float maxValue, int currentLevel)
    {
        slider.value = currentValue / maxValue;
        lvlAnzeige.text = "Level: " + currentLevel;
    }
}
