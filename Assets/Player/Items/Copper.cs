using UnityEngine;
using System;

namespace Player.Items
{
    public class Copper : MonoBehaviour, ICollectible
    {
        public static event Action OnCopperCollected;
        public int dropChance = 90;
        
        private Rigidbody2D rb;
        private bool hasTarget;
        private Vector3 TargetPosition;
        public int getDropChance()
        {
            return dropChance;
        }
        public void Collect()
        {
            Destroy(gameObject);
            OnCopperCollected?.Invoke();
        }
        private void FixedUpdate()
        {
            if (hasTarget)
            {
                Vector2 targetDirection = (TargetPosition - transform.position).normalized;
                rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * 5;
            }
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void SetTarget(Vector3 position)
        {
            TargetPosition = position;
            hasTarget = true;
        }
    }
}
