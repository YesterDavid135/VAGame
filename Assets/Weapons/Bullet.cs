using Enemies;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rb2d;
    public int damage = 1;
    public bool isExplosive = false;

    public float explosionRadius = 2f; // Adjust the radius as needed
    public int explosionDamage = 5;
    public GameObject explosion;

    private void OnEnable() {
        if (rb2d != null) {
            rb2d.velocity = Vector2.up;
        }

        Invoke("Disable", 1f);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (isExplosive) {
            // Check if the bullet is explosive and apply damage to all nearby objects.
            Explode(transform.position);
        }
        else {
            // If not explosive, handle collisions as before.
            switch (other.gameObject.tag) {
                case "Wall":
                    Disable();
                    break;
                case "Enemy":
                    Disable();
                    break;
            }
        }
    }

    void Explode(Vector3 position) {
        Instantiate(explosion, position, Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders) {
            // Check if the hit object implements the IDamageable interface
            IEnemy damageable = hit.GetComponent<IEnemy>();
            if (damageable != null) {
                damageable.TakeDamage(explosionDamage);
            }
        }

        Disable(); // Disable the projectile after the explosion
    }

    void Disable() {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }
}