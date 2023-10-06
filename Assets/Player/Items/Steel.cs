using UnityEngine;
using System;

namespace Player.Items
{
    public class Steel : MonoBehaviour, ICollectible
    {
        public static event Action OnSteelCollected;
        public int dropChance = 80;

        public int getDropChance()
        {
            return dropChance;
        }

        public void Collect()
        {
            Destroy(gameObject);
            OnSteelCollected?.Invoke();
        }
    }
}
