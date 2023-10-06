using UnityEngine;
using System;

namespace Player.Items
{
    public class Steel : MonoBehaviour, ICollectible
    {
        public static event Action OnSteelCollected;
        public void Collect()
        {
            Destroy(gameObject);
            OnSteelCollected?.Invoke();
        }
    }
}
