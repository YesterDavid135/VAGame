using UnityEngine;
using System;

namespace Player.Items
{
    public class Copper : MonoBehaviour, ICollectible
    {
        public static event Action OnCopperCollected;
        public int dropChance = 90;
        public int getDropChance()
        {
            return dropChance;
        }
        public void Collect()
        {
            Destroy(gameObject);
            OnCopperCollected?.Invoke();
        }
    }
}
