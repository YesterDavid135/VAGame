using System;
using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    [Header("Player Settings")]
    public Camera sceneCamera;
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public Weapon weapon;
    public int lvlPoints = 0;
    
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private float timeBetweenHeal = 1;

    [SerializeField] public float xpMultiplier = 1,naturalRegenPerSec = 1,health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;
    [SerializeField] private XPBar XPBar;
    
    [Header("Experience Manager")]
    [SerializeField] private TextMeshProUGUI levelPoints;
    [SerializeField] private int currentExperience, maxExperience, currentLevel;

    [Header("Dash Settings")] 
    [SerializeField] public float dashSpeed = 10;
    [SerializeField] public float dashDuration = 1;
    [SerializeField] public float dashCooldown = 1;
    private bool isDashing = false;
    private bool canDash = true;
    
    private void Start()
    {
        canDash = true;
        health = maxHealth;
        Healthbar.UpdateHealthBar(health, maxHealth);
        XPBar.UpdateXPBar(currentExperience,maxExperience,currentLevel);
        levelPoints.text = "U-Points: "+lvlPoints;
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
        
        if (Input.GetMouseButton(0)) {
            weapon.Fire(8);
        }
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move() {
        if (Input.GetMouseButton(1) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // Rotate
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
    private IEnumerator Dash()
    {
        Debug.Log("Should Dash");
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * 999, moveDirection.y * 999);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
        currentExperience +=  Mathf.RoundToInt(newExperience *xpMultiplier);
        XPBar.UpdateXPBar(currentExperience,maxExperience,currentLevel);
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        lvlPoints++;
        levelPoints.text = "U-Points: "+lvlPoints;
        currentLevel++;
        currentExperience = 0;
        maxExperience += (maxExperience / 50);
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