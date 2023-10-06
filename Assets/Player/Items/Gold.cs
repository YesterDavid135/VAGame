using UnityEngine;
using System;

namespace Player.Items
{
    public class Gold : MonoBehaviour, ICollectible
    {
        public static event Action OnGoldCollected;
        public void Collect()
        {
            Destroy(gameObject);
            OnGoldCollected?.Invoke();
        }
    }
}
