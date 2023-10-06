using UnityEngine;
using System;

namespace Player.Items
{
    public class Gold : MonoBehaviour, ICollectible
    {
        public static event Action OnGoldCollected;
        public int dropChance = 5;
        public int getDropChance()
        {
            return dropChance;
        }
        public void Collect()
        {
            Destroy(gameObject);
            OnGoldCollected?.Invoke();
        }
    }
}
