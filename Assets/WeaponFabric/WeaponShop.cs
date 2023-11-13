using TMPro;
using UnityEngine;

public class WeaponShop : MonoBehaviour {
    [Header("Player")] public PlayerController player;


    [Header("Labels")] public TextMeshProUGUI akBuyButton;
    public TextMeshProUGUI shotgunBuyButton;
    public TextMeshProUGUI rocketLauncherBuyButton;


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
}