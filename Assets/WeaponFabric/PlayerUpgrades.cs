using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{    
    public  PlayerController playerValues;

    public TextMeshProUGUI pointsText; // Reference to a UI Text component that displays available points
    public TextMeshProUGUI uCostsHealth; 
    public TextMeshProUGUI uCostsSpeed; 
    public TextMeshProUGUI uCostsRegen; 
    public TextMeshProUGUI uCostsXpMultiplier;
    
    public TextMeshProUGUI currentHealth; 
    public TextMeshProUGUI currentSpeed; 
    public TextMeshProUGUI currentRegen; 
    public TextMeshProUGUI currentXpMultiplier;
    
    public int healthUpgradePrice = 1;
    public int speedUpgradePrice = 2;
    public int regenerationUpgradePrice = 3;
    public int xpMultiplierUpgradePrice = 4;

    
    private void FixedUpdate()
    {
        // Update the available points text
        pointsText.text = "U-Points: " + playerValues.lvlPoints.ToString();

    }

    private void OnEnable()
    {
        currentHealth.text = $"({playerValues.maxHealth})";
        currentSpeed.text = $"({playerValues.moveSpeed})";
        currentRegen.text = $"({playerValues.naturalRegenPerSec})";
        currentXpMultiplier.text = $"({playerValues.xpMultiplier})";
    }

    public void BuyHealthUpgrade()
    {
        if (playerValues.lvlPoints >= healthUpgradePrice)
        {
            currentHealth.text = $"({playerValues.maxHealth})";
            playerValues.lvlPoints -= healthUpgradePrice;
            healthUpgradePrice++;
            uCostsHealth.text = "Buy for " + healthUpgradePrice;
            playerValues.maxHealth += 5;
            currentHealth.text = $"({playerValues.maxHealth})";
        }
    }

    public void BuySpeedUpgrade()
    {
        if (playerValues.lvlPoints >= speedUpgradePrice)
        {
            currentSpeed.text = $"({playerValues.moveSpeed})";
            playerValues.lvlPoints -= speedUpgradePrice;
            speedUpgradePrice++;
            uCostsSpeed.text = "Buy for " + speedUpgradePrice;
            playerValues.moveSpeed += (float) 0.25;
            currentSpeed.text = $"({playerValues.moveSpeed})";
        }
    }

    public void BuyRegenerationUpgrade()
    {
        if (playerValues.lvlPoints >= regenerationUpgradePrice)
        {
            currentRegen.text = $"({playerValues.naturalRegenPerSec})";
            playerValues.lvlPoints -= regenerationUpgradePrice;
            regenerationUpgradePrice++;
            uCostsRegen.text = "Buy for " + regenerationUpgradePrice;
            playerValues.naturalRegenPerSec += (float) 0.1;
            currentRegen.text = $"({playerValues.naturalRegenPerSec})";
        }
    }

    public void BuyXpMultiplierUpgrade()
    {
        if (playerValues.lvlPoints >= xpMultiplierUpgradePrice)
        {
            currentXpMultiplier.text = $"({playerValues.xpMultiplier})";
            playerValues.lvlPoints -= xpMultiplierUpgradePrice;
            xpMultiplierUpgradePrice++;
            uCostsXpMultiplier.text = "Buy for " + xpMultiplierUpgradePrice;
            playerValues.xpMultiplier += (float)0.25;
            currentXpMultiplier.text = $"({playerValues.xpMultiplier})";
        }
    }
}