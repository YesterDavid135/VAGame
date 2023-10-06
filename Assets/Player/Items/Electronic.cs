using UnityEngine;
using System;

namespace Player.Items
{
    public class Electronics : MonoBehaviour, ICollectible
    {
        public static event Action OnElectronicCollected;
        public void Collect()
        {
            Destroy(gameObject);
            OnElectronicCollected?.Invoke();
        }
    }
}
