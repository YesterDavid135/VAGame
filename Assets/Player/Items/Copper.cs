using UnityEngine;
using System;

namespace Player.Items
{
    public class Copper : MonoBehaviour, ICollectible
    {
        public static event Action OnCopperCollected;
        public void Collect()
        {
            Destroy(gameObject);
            OnCopperCollected?.Invoke();
        }
    }
}
