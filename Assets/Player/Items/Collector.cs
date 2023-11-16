using UnityEngine;

namespace Player.Items
{
    public class Collector : MonoBehaviour
    {
        public PlayerController player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollectible collectible = collision.GetComponent<ICollectible>();
            if (collectible != null)
            {
                if (collectible is Copper)
                {
                    player.IncrementCopper();
                }
                else if (collectible is Gold)
                {
                    player.IncrementGold();
                }
                else if (collectible is Electronics)
                {
                    player.IncrementElectronic();
                }
                else if (collectible is Steel)
                {
                    player.IncrementSteel();
                }
                collectible.Collect();
            }
        }
    }
}
