using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Weapon weapon;

    [SerializeField] private float naturalRegenPerSec = 1,health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;
    [SerializeField] private int currentExperience, maxExperience, currentLevel;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    private float timeBetweenHeal = 1;
    public static event Action OnPlayerDeath;

    // Update is called once per frame
    private void Start() {
        Healthbar = GetComponentInChildren<FloatingHealthbar>();
        health = maxHealth;
        Healthbar.UpdateHealthBar(health, maxHealth);
    }

    void FixedUpdate() {
        ProcessInputs();
        Move();
        if (timeBetweenHeal <= 0) {
            if (health < maxHealth - naturalRegenPerSec)
            {
                health += naturalRegenPerSec;
                Healthbar.UpdateHealthBar(health, maxHealth);
            }else if (health < maxHealth)
            {
                health = maxHealth;
                Healthbar.UpdateHealthBar(health, maxHealth);
            }
            timeBetweenHeal = 1;
        }
        else {
            timeBetweenHeal -= Time.deltaTime;
        }
       
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

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        maxHealth += 10;
        currentLevel++;
        currentExperience = 0;
        maxExperience += 100;
        
        Healthbar.UpdateHealthBar(health, maxHealth);
    }
    
    public void TakeDamage(float damage) {
        Debug.Log($"Damage Amount: {damage}");
        health -= damage;
        Healthbar.UpdateHealthBar(health, maxHealth);
        Debug.Log($"Health is now {health}");
        if (health <= 0) {
            health = 0;
            SceneManager.LoadScene(0);
        }
    }
}