using System;
using TMPro;
using UnityEngine;

public class WeaponShop : MonoBehaviour {
    [Header("Player")] public PlayerController player;


    [Header("Labels")] public TextMeshProUGUI akBuyButton;
    public TextMeshProUGUI shotgunBuyButton;
    public TextMeshProUGUI rocketLauncherBuyButton;

    [Header("AkPrices")] public float akBuyPrice = 5;
    public float akSpeedUpgradePrice = 10;
    public float akDamageUpgradePrice = 10;
    public float akDoubleshotUpgradePrice = 10;


    public void BuyAk() {
        if (player.BuyAk47()) {
            akBuyButton.text = "Unlocked";
        }
    }

    public void BuyShotgun() {
        if (player.BuyShotgun()) {
            shotgunBuyButton.text = "Unlocked";
        }
    }

    public void BuyRocketLauncher() {
        if (player.BuyRocketLauncher()) {
            rocketLauncherBuyButton.text = "Unlocked";
        }
    }

    public void UpgradeAk(TextMeshProUGUI text) {
        float price;

        switch (text.name) {
            case "Speed":
                price = akSpeedUpgradePrice;
                if (player.UpgradeAk(text.name, price)) {
                    akSpeedUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + akSpeedUpgradePrice;
                }
                break;
            case "Damage":
                price = akDamageUpgradePrice;
                if (player.UpgradeAk(text.name, price)) {
                    akDamageUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + akDamageUpgradePrice;
                }
                break;
            case "Doubleshot":
                price = akDoubleshotUpgradePrice;
                if (player.UpgradeAk(text.name, price)) {
                    akDoubleshotUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + akDoubleshotUpgradePrice;
                }
                break;
        }

    }
}