using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons.Ak47;

public class WeaponShop : MonoBehaviour {
    [Header("Player")] public PlayerController player;


    [Header("Labels")] 
    public TextMeshProUGUI akBuyButton;
    public TextMeshProUGUI shotgunBuyButton;
    public TextMeshProUGUI rocketLauncherBuyButton;

    [Header("AkPrices")] public int akBuyPrice = 50;
    public float akSpeedUpgradePrice = 10;
    public float akDamageUpgradePrice = 25;
    public float akDoubleshotUpgradePrice = 1;

    [Header("ShotgunPrices")] public int shotgunBuyPrice = 250;
    public float shotgunSpeedUpgradePrice = 10;
    public float shotgunDamageUpgradePrice = 35;
    public float shotgunDoubleshotUpgradePrice = 1;

    [Header("RocketLauncherPrices")] public int rocketLauncherBuyPrice = 450;
    public float rocketLauncherSpeedUpgradePrice = 15;
    public float rocketLauncherDamageUpgradePrice = 40;
    public float rocketLauncherDoubleshotUpgradePrice = 1;

    public void BuyAk() {
        if (player.BuyAk47(akBuyPrice)) {
            akBuyButton.text = "Unlocked";
            akBuyButton.alignment = TextAlignmentOptions.Center;
            akBuyButton.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void BuyShotgun() {
        if (player.BuyShotgun(shotgunBuyPrice)) {
            shotgunBuyButton.text = "Unlocked";
            shotgunBuyButton.alignment = TextAlignmentOptions.Center;
            shotgunBuyButton.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void BuyRocketLauncher() {
        if (player.BuyRocketLauncher(rocketLauncherBuyPrice)) {
            rocketLauncherBuyButton.text = "Unlocked";
            rocketLauncherBuyButton.alignment = TextAlignmentOptions.Center;
            rocketLauncherBuyButton.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void UpgradeAk(TextMeshProUGUI text) {
        float price;

        switch (text.name) {
            case "Speed":
                price = akSpeedUpgradePrice;
                if (player.UpgradeAk(text.name, (int)price)) {
                    akSpeedUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + (int)akSpeedUpgradePrice;
                }

                break;
            case "Damage":
                price = akDamageUpgradePrice;
                if (player.UpgradeAk(text.name, (int)price)) {
                    akDamageUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + (int)akDamageUpgradePrice;
                }

                break;
            case "Doubleshot":
                price = akDoubleshotUpgradePrice;
                if (player.UpgradeAk(text.name, (int)price)) {
                    akDoubleshotUpgradePrice *= 1.1f;
                    text.text = "Upgrade for " + (int)akDoubleshotUpgradePrice;
                }

                break;
        }
    }

    public void UpgradeShotgun(TextMeshProUGUI text) {
        float price;

        switch (text.name) {
            case "Speed":
                price = shotgunSpeedUpgradePrice;
                if (player.UpgradeShotgun(text.name, (int)price)) {
                    shotgunSpeedUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + (int)shotgunSpeedUpgradePrice;
                }

                break;
            case "Damage":
                price = shotgunDamageUpgradePrice;
                if (player.UpgradeShotgun(text.name, (int)price)) {
                    shotgunDamageUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + (int)shotgunDamageUpgradePrice;
                }

                break;
            case "Scatter":
                price = shotgunDoubleshotUpgradePrice;
                if (player.UpgradeShotgun(text.name, (int)price)) {
                    shotgunDoubleshotUpgradePrice *= 1.1f;
                    text.text = "Upgrade for " + (int)shotgunDoubleshotUpgradePrice;
                }

                break;
        }
    }

    public void UpgradeRocketLauncher(TextMeshProUGUI text) {
        float price;

        switch (text.name) {
            case "Speed":
                price = rocketLauncherSpeedUpgradePrice;
                if (player.UpgradeRocketLauncher(text.name, (int)price)) {
                    rocketLauncherSpeedUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + (int)rocketLauncherSpeedUpgradePrice;
                }

                break;
            case "Damage":
                price = rocketLauncherDamageUpgradePrice;
                if (player.UpgradeRocketLauncher(text.name, (int)price)) {
                    rocketLauncherDamageUpgradePrice *= 1.2f;
                    text.text = "Upgrade for " + (int)rocketLauncherDamageUpgradePrice;
                }

                break;
            case "Explosion":
                price = rocketLauncherDoubleshotUpgradePrice;
                if (player.UpgradeRocketLauncher(text.name, (int)price)) {
                    rocketLauncherDoubleshotUpgradePrice *= 1.1f;
                    text.text = "Upgrade for " + (int)rocketLauncherDoubleshotUpgradePrice;
                }

                break;
        }
    }
}