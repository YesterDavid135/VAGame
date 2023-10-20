using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Weapons;
public class PlayerController : MonoBehaviour {
    [Header("Player Settings")]
    public Camera sceneCamera;
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public IWeapon weapon;
    public int lvlPoints = 0;

    [Header("Weapon Settings")]
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject ak47;
    public GameObject rocketlauncher;

    private bool key1Pistol = false;
    private bool key2AK47 = false;
    private bool key3Shotgun = false;
    private bool key4RocketLauncher = false;
    private bool mousebutton0Fire = false;
    private bool keyhHeal = false;
    
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
    
    [Header("Heal Settings")]
    public float healAmount = 25.0f;
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
        SetAllWeaponsInactive();
    }
    private void SetAllWeaponsInactive() {
        pistol.SetActive(false);
        ak47.SetActive(false);
        shotgun.SetActive(false);
        rocketlauncher.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            key1Pistol = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            key2AK47 = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            key3Shotgun = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            key4RocketLauncher = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            keyhHeal = true;
        }

        if (Input.GetMouseButton(0))
        {
            mousebutton0Fire = true;
        }
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
        if (key1Pistol) {
            Debug.Log("Equipping Pistol");
            weapon = pistol.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            pistol.SetActive(true);
            key1Pistol = false;
        }

        if (key2AK47) {
            Debug.Log("Equipping AK47");
            weapon = ak47.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            ak47.SetActive(true);
            key2AK47 = false;
        }

        if (key3Shotgun) {
            Debug.Log("Equipping Shotgun");
            weapon = shotgun.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            shotgun.SetActive(true);
            key3Shotgun = false;
        }
        if (key4RocketLauncher) {
            Debug.Log("Equipping Rocket Launcher");
            weapon = rocketlauncher.GetComponent<IWeapon>(); // Change to the desired weapon type
            SetAllWeaponsInactive();
            rocketlauncher.SetActive(true);
            key4RocketLauncher = false;
        }
        
        if (mousebutton0Fire && weapon != null) {
            weapon.Fire(8);
            mousebutton0Fire = false;
        }
        else
        {
            mousebutton0Fire = false;
        }

        if (keyhHeal && Time.time - lastHealTime >= healCooldown)
        {
            Heal();
            keyhHeal = false;
        }
        else
        {
            keyhHeal = false;
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