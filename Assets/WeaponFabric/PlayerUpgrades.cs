using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {
    [Header("Player")] public PlayerController playerValues;


    [Header("Labels")]
    public TextMeshProUGUI pointsText; // Reference to a UI Text component that displays available points

    public TextMeshProUGUI uCostsHealth;
    public TextMeshProUGUI uCostsSpeed;
    public TextMeshProUGUI uCostsRegen;
    public TextMeshProUGUI uCostsXpMultiplier;
    public TextMeshProUGUI costMagnet;

    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI currentSpeed;
    public TextMeshProUGUI currentRegen;
    public TextMeshProUGUI currentXpMultiplier;
    public TextMeshProUGUI currentMagnet;

    public TextMeshProUGUI availablePointsDisplay;

    [Header("Initial Upgrade Prices")] public int healthUpgradePrice = 1;
    public int speedUpgradePrice = 1;
    public int regenerationUpgradePrice = 1;
    public int xpMultiplierUpgradePrice = 1;
    public int magnetUpgradePrice = 1;


    private void FixedUpdate() {
        // Update the available points text
        pointsText.text = "U-Points: " + playerValues.lvlPoints.ToString();
        availablePointsDisplay.text = "Available Points:" + playerValues.lvlPoints.ToString();
    }

    private void OnEnable() {
        currentHealth.text = $"({playerValues.maxHealth})";
        currentSpeed.text = $"({playerValues.moveSpeed})";
        currentRegen.text = $"({playerValues.naturalRegenPerSec})";
        currentXpMultiplier.text = $"({playerValues.xpMultiplier})";
        currentMagnet.text = $"({(int)playerValues.magnetCollider.radius})";
    }

    public void BuyHealthUpgrade() {
        if (playerValues.lvlPoints >= healthUpgradePrice) {
            currentHealth.text = $"({playerValues.maxHealth})";
            playerValues.lvlPoints -= healthUpgradePrice;
            healthUpgradePrice++;
            uCostsHealth.text = "Buy for " + healthUpgradePrice;
            playerValues.maxHealth += 5;
            currentHealth.text = $"({playerValues.maxHealth})";
        }

        pointsText.text = "U-Points: " + playerValues.lvlPoints.ToString();
        availablePointsDisplay.text = "Available Points:" + playerValues.lvlPoints.ToString();
    }

    public void BuySpeedUpgrade() {
        if (playerValues.lvlPoints >= speedUpgradePrice) {
            currentSpeed.text = $"({playerValues.moveSpeed})";
            playerValues.lvlPoints -= speedUpgradePrice;
            speedUpgradePrice++;
            uCostsSpeed.text = "Buy for " + speedUpgradePrice;
            playerValues.moveSpeed += (float)0.75;
            currentSpeed.text = $"({playerValues.moveSpeed})";
        }

        pointsText.text = "U-Points: " + playerValues.lvlPoints.ToString();
        availablePointsDisplay.text = "Available Points:" + playerValues.lvlPoints.ToString();
    }

    public void BuyRegenerationUpgrade() {
        if (playerValues.lvlPoints >= regenerationUpgradePrice) {
            currentRegen.text = $"({playerValues.naturalRegenPerSec})";
            playerValues.lvlPoints -= regenerationUpgradePrice;
            regenerationUpgradePrice++;
            uCostsRegen.text = "Buy for " + regenerationUpgradePrice;
            playerValues.naturalRegenPerSec += (float)0.25;
            currentRegen.text = $"({playerValues.naturalRegenPerSec})";
        }

        pointsText.text = "U-Points: " + playerValues.lvlPoints.ToString();
        availablePointsDisplay.text = "Available Points:" + playerValues.lvlPoints.ToString();
    }

    public void BuyXpMultiplierUpgrade() {
        if (playerValues.lvlPoints >= xpMultiplierUpgradePrice) {
            currentXpMultiplier.text = $"({playerValues.xpMultiplier})";
            playerValues.lvlPoints -= xpMultiplierUpgradePrice;
            xpMultiplierUpgradePrice++;
            uCostsXpMultiplier.text = "Buy for " + xpMultiplierUpgradePrice;
            playerValues.xpMultiplier += (float)1.5;
            currentXpMultiplier.text = $"({playerValues.xpMultiplier})";
        }

        pointsText.text = "U-Points: " + playerValues.lvlPoints.ToString();
        availablePointsDisplay.text = "Available Points:" + playerValues.lvlPoints.ToString();
    }

    public void BuyMagnetUpgrade() {
        if (playerValues.electronicCount >= magnetUpgradePrice) {
            playerValues.electronicCount -= magnetUpgradePrice;
            playerValues.magnetCollider.radius *= 1.5f;
            magnetUpgradePrice++;
            costMagnet.text = "Buy for " + magnetUpgradePrice;
            currentMagnet.text = $"({(int)playerValues.magnetCollider.radius})";
        }
    }
}