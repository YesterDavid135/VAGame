using System;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;
using Weapons.Pistol;

public class EnemyLongRange : MonoBehaviour {
    public static event Action<EnemyLongRange> OnEnemyKilled;
    [SerializeField] private float health, maxHealth = 20f;
    [SerializeField] private FloatingHealthbar Healthbar;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public Pistol weapon;


    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public Rigidbody2D rb;

    private int expAmount = 20;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Healthbar = GetComponentInChildren<FloatingHealthbar>();
        health = maxHealth;
        timeBetweenShots = startTimeBetweenShots;
        Healthbar.UpdateHealthBar(health, maxHealth);
    }

    private void FixedUpdate() {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance) {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime));
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance &&
                 Vector2.Distance(transform.position, player.position) > retreatDistance) {
            rb.MovePosition(this.transform.position);
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance) {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime));
        }

        Vector2 aimDirection = (Vector2)player.position - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;

        if (timeBetweenShots <= 0) {
            weapon.Fire(9);

            timeBetweenShots = startTimeBetweenShots;
        }
        else {
            timeBetweenShots -= Time.deltaTime;
        }
    }


    public void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Bullet":
                TakeDamage(1);
                break;
        }
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;

        if (health <= 0) {
            ExperienceManager.Instance.AddExperience(expAmount);
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
        else
        {
            Healthbar.UpdateHealthBar(health, maxHealth);
        }
    }
}