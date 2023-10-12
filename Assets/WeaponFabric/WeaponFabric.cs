using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFabric : MonoBehaviour
{
    public GameObject shopInterface; // Reference to your shop interface Canvas or GameObject

    private void OnMouseDown()
    {
        // Check if the shop interface is not already active
        if (!shopInterface.activeSelf)
        {
            // Open the shop interface
            shopInterface.SetActive(true);
        }
    }
    
    public void CloseShop()
    {
        // Close the shop interface
        shopInterface.SetActive(false);
    }
}
