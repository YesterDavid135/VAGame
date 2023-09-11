using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rb2d;

    private void OnEnable() {
        if (rb2d != null) {
            rb2d.velocity = Vector2.up;
        }

        Invoke("Disable", 1f);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Wall":
                Disable();
                break;
            case "Enemy":
                Disable();
                break;
        }
    }

    void Disable() {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }
}