using System;
using TMPro;
using UnityEngine;
using Weapons.Ak47;

public class WeaponShop : MonoBehaviour {
    [Header("Player")] public PlayerController player;


    [Header("Labels")] public TextMeshProUGUI akBuyButton;
    public TextMeshProUGUI shotgunBuyButton;
    public TextMeshProUGUI rocketLauncherBuyButton;

    [Header("AkPrices")] public float akBuyPrice = 50;
    public float akSpeedUpgradePrice = 10;
    public float akDamageUpgradePrice = 25;
    public float akDoubleshotUpgradePrice = 50;
    
    [Header("ShotgunPrices")] public float shotgunBuyPrice = 250;
    public float shotgunSpeedUpgradePrice = 10;
    public float shotgunDamageUpgradePrice = 35;
    public float shotgunDoubleshotUpgradePrice = 150;
    
    [Header("RocketLauncherPrices")] public float rocketLauncherBuyPrice = 450;
    public float rocketLauncherSpeedUpgradePrice = 15;
    public float rocketLauncherDamageUpgradePrice = 40;
    public float rocketLauncherDoubleshotUpgradePrice = 100;

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

    public void UpgradeShotgun(TextMeshProUGUI text) {
        float price;

        switch (text.name) {
            case "Speed":
                price = shotgunSpeedUpgradePrice;
                if (player.UpgradeShotgun(text.name, price)) {
                    shotgunSpeedUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + shotgunSpeedUpgradePrice;
                }
                break;
            case "Damage":
                price = shotgunDamageUpgradePrice;
                if (player.UpgradeShotgun(text.name, price)) {
                    shotgunDamageUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + shotgunDamageUpgradePrice;
                }
                break;
            case "Scatter":
                price = shotgunDoubleshotUpgradePrice;
                if (player.UpgradeShotgun(text.name, price)) {
                    shotgunDoubleshotUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + shotgunDoubleshotUpgradePrice;
                }
                break;
        }

    }
    public void UpgradeRocketLauncher(TextMeshProUGUI text) {
        float price;

        switch (text.name) {
            case "Speed":
                price = rocketLauncherSpeedUpgradePrice;
                if (player.UpgradeRocketLauncher(text.name, price)) {
                    rocketLauncherSpeedUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + rocketLauncherSpeedUpgradePrice;
                }
                break;
            case "Damage":
                price = rocketLauncherDamageUpgradePrice;
                if (player.UpgradeRocketLauncher(text.name, price)) {
                    rocketLauncherDamageUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + rocketLauncherDamageUpgradePrice;
                }
                break;
            case "Explosion":
                price = rocketLauncherDoubleshotUpgradePrice;
                if (player.UpgradeRocketLauncher(text.name, price)) {
                    rocketLauncherDoubleshotUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + rocketLauncherDoubleshotUpgradePrice;
                }
                break;
        }

    }
}