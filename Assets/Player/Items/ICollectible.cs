using UnityEngine;

namespace Player.Items
{
    public interface ICollectible
    {
        public int getDropChance();
        public void Collect();
        public void SetTarget(Vector3 position);
    }
}
