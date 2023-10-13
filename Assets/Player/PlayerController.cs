using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Weapons;

public class PlayerController : MonoBehaviour {
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public IWeapon weapon;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject ak47;

    [SerializeField] private float naturalRegenPerSec = 1, health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;
    [SerializeField] private XPBar XPBar;
    [SerializeField] private int currentExperience, maxExperience, currentLevel;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    private float timeBetweenHeal = 1;

    [Header("Dash Settings")] [SerializeField]
    public float dashSpeed = 10;

    [SerializeField] public float dashDuration = 1;
    [SerializeField] public float dashCooldown = 1;
    private bool isDashing = false;
    private bool canDash = true;

    private void Start() {
        canDash = true;
        health = maxHealth;
        Healthbar.UpdateHealthBar(health, maxHealth);
        XPBar.UpdateXPBar(currentExperience, maxExperience, currentLevel);

        SetAllWeaponsInactive();
    }

    private void SetAllWeaponsInactive() {
        pistol.SetActive(false);
        ak47.SetActive(false);
        shotgun.SetActive(false);
    }

    void FixedUpdate() {
        ProcessInputs();
        Move();
        if (timeBetweenHeal <= 0) {
            if (health < maxHealth - naturalRegenPerSec) {
                health += naturalRegenPerSec;
                Healthbar.UpdateHealthBar(health, maxHealth);
            }
            else if (health < maxHealth) {
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

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("Equipping Pistol");
            weapon = pistol.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            pistol.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Equipping AK47");
            weapon = ak47.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            ak47.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log("Equipping Shotgun");
            weapon = shotgun.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            shotgun.SetActive(true);
        }

        if (Input.GetMouseButton(0)) {
            // Fire the current weapon
            if (weapon != null) {
                weapon.Fire(8); // Call the Fire method of the current weapon
            }
            else {
                Debug.Log("No Weapon Equipped");
            }
        }

        if (Input.GetMouseButton(1) && canDash) {
            StartCoroutine(Dash());
        }
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

    private void OnEnable() {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable() {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExperience) {
        currentExperience += newExperience;
        XPBar.UpdateXPBar(currentExperience, maxExperience, currentLevel);
        if (currentExperience >= maxExperience) {
            LevelUp();
        }
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed * 10, moveDirection.y * moveSpeed * 10);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void LevelUp() {
        maxHealth += 10;
        currentLevel++;
        currentExperience = 0;
        maxExperience += 100;

        Healthbar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Healthbar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) {
            health = 0;
            SceneManager.LoadScene(0);
        }
    }
}