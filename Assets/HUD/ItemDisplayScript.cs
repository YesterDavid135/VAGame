using System;
using System.Collections;
using System.Collections.Generic;
using Player.Items;
using TMPro;
using UnityEngine;

public class ItemDisplayScript : MonoBehaviour {
    public PlayerController playerValues;

    public TextMeshProUGUI countText;
    public String collectible;

    // Update is called once per frame
    void Update() {
        switch (collectible) {
            case "Copper":
                countText.text = playerValues.copperCount.ToString();
                break;
            case "Steel":
                countText.text = playerValues.steelCount.ToString();
                break;
            case "Gold":
                countText.text = playerValues.goldCount.ToString();
                break;
            case "Electronic":
                countText.text = playerValues.electronicCount.ToString();
                break;
        }
    }
}