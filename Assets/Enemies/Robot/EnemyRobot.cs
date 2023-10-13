using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyRobot : MonoBehaviour
{
    public static event Action<EnemyRobot> OnEnemyKilled;
    [SerializeField] private float health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;

    public float speed;
    public float Damage;
    public float stoppingDistance;
    public Transform playerPos;
    public PlayerController player;

    public ParticleSystem deathParticles;
    public PlayerController playerToDamage;
    private bool isColliding = false;
    private float timeBetweenDamage;
    public float startTimeBetweenDamage;

    public Rigidbody2D rb;

    private int expAmount = 10;
    
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Healthbar = GetComponentInChildren<FloatingHealthbar>();
        health = maxHealth;
        timeBetweenDamage = startTimeBetweenDamage;
        Healthbar.UpdateHealthBar(health, maxHealth);
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > stoppingDistance)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime));
        }

        if (timeBetweenDamage <= 0)
        {
            if (isColliding)
            {
                playerToDamage.TakeDamage(Damage);
                timeBetweenDamage = startTimeBetweenDamage;
            }
        }
        else
        {
            timeBetweenDamage -= Time.deltaTime;
        }

        Vector2 aimDirection = (Vector2)playerPos.position - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                TakeDamage(1);
                break;
            case "Player":
                playerToDamage = other.GetComponentInParent<PlayerController>();
                isColliding = true;
                break;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        if (health <= 0)
        {
            ExperienceManager.Instance.AddExperience(expAmount);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            OnEnemyKilled?.Invoke(this);
        }
        else
        {
            Healthbar.UpdateHealthBar(health, maxHealth);
        }
    }
}