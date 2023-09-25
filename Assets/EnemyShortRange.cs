using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyShortRange : MonoBehaviour {
    public static event Action<EnemyShortRange> OnEnemyKilled;
    [SerializeField] private float health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;

    public float speed;
    public float stoppingDistance;
    public Transform playerPos;
    public PlayerController player;


    public Transform attackPos;
    public LayerMask whatIsPlayer;
    
    private float timeBetweenDamage;
    public float startTimeBetweenDamage;

    public Rigidbody2D rb;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Healthbar = GetComponentInChildren<FloatingHealthbar>();
        health = maxHealth;
        timeBetweenDamage = startTimeBetweenDamage;
        Healthbar.UpdateHealthBar(health, maxHealth);
    }

    private void FixedUpdate() {
        if (Vector2.Distance(transform.position, playerPos.position) > stoppingDistance) {
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime));
        }

        Vector2 aimDirection = (Vector2)playerPos.position - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }


    public void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Bullet":
                TakeDamage(1);
                break;
            case "Player":
                if (timeBetweenDamage <= 0)
                {
                    Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(attackPos.position, 5, whatIsPlayer);
                    for (int i = 0; i < playersToDamage.Length; i++)
                    {
                        playersToDamage[i].GetComponent<PlayerController>().TakeDamage(1);
                    }
                    
                    timeBetweenDamage = startTimeBetweenDamage;
                }
                else {
                    timeBetweenDamage -= Time.deltaTime;
                }
                break;
        }
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;

        Healthbar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
}