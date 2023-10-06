using UnityEngine;
using System;

namespace Player.Items
{
    public class Electronics : MonoBehaviour, ICollectible
    {
        public static event Action OnElectronicCollected;
        public int dropChance = 50;
        public int getDropChance()
        {
            return dropChance;
        }
        public void Collect()
        {
            Destroy(gameObject);
            OnElectronicCollected?.Invoke();
        }
    }
}
