using System;
using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [Header("Player Settings")]
    public Camera sceneCamera;
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public Weapon weapon;
    public int lvlPoints = 0;

    public float healAmount = 20.0f;
    
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
    private bool canDash = true;
    
    public Image heartImage;
    public float healCooldown = 20.0f; 
    private float lastHealTime; 
    private Coroutine cooldownCoroutine;
    
    private void Start()
    {
        lastHealTime = -healCooldown;
        UpdateHeartFill();
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
        if (Input.GetKeyDown(KeyCode.H) && Time.time - lastHealTime >= healCooldown)
        {
            Heal();
        }
        if (Input.GetMouseButton(1) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

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
    private IEnumerator Dash()
    {
        canDash = false;
        moveSpeed += dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed -= dashSpeed;
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
    void Heal()
    {
        health += healAmount; 
        health = Mathf.Min(health, maxHealth);
        Healthbar.UpdateHealthBar(health,maxHealth);
        lastHealTime = Time.time;
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        cooldownCoroutine = StartCoroutine(CooldownFill());
    }

    void UpdateHeartFill()
    {
        float timeSinceLastHeal = Time.time - lastHealTime;
        float fillAmount = Mathf.Clamp01(timeSinceLastHeal / healCooldown);
        heartImage.fillAmount = fillAmount;
    }

    IEnumerator CooldownFill()
    {
        float startTime = Time.time;
        float endTime = startTime + healCooldown;

        while (Time.time < endTime)
        {
            float elapsed = Time.time - startTime;
            float progress = elapsed / healCooldown;
            heartImage.fillAmount = Mathf.Lerp(0.0f, 1.0f, progress);
            yield return null;
        }
        heartImage.fillAmount = 1.0f; 
    }
}