using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Weapon weapon;

    [SerializeField] private float health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    // Update is called once per frame
    private void Start() {
        Healthbar = GetComponentInChildren<FloatingHealthbar>();
        health = maxHealth;
        Healthbar.UpdateHealthBar(health, maxHealth);
    }

    void FixedUpdate() {
        ProcessInputs();
        Move();
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0)) {
            weapon.Fire(8);
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // Rotate
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Bullet":
                TakeDamage(1);
                break;
        }
    }

    public void TakeDamage(float damage) {
        Debug.Log($"Damage Amount: {damage}");
        health -= damage;
        Healthbar.UpdateHealthBar(health, maxHealth);
        Debug.Log($"Health is now {health}");
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}