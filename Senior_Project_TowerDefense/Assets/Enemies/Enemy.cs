using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 2f;
    private float backupSpeed;
    public float currentHealth = 50f;
    private float maxHealth;
    public int enemyValue;

    private float slowDuration = 1f;//these two need to be moved to the tower eventually
    private float slowBase = 1f;
    private Vector3 scaleChange;

    public GameObject healthObject; // the object whose health will be decreased
    public Transform healthBar;

    [Header("Unity requirements")]
    private Health healthSystem; // reference to the health system script
    private Transform target;
    private int waypointIndex = 0;
    private Health lives;
    private bool slowed = false;
    public string enemyTag = "Enemy";

    


    void Start()
    {
        target = Waypoints.points[0];
        healthSystem = healthObject.GetComponent<Health>();
        backupSpeed = speed;
        maxHealth = currentHealth;
    }

    void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        if (Health.currentHealth <= 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject enemy in enemies)
            {
                Destroy(gameObject);
            }
        }
        if (slowed)
        {
            if (slowBase <= 0f)
            {
                slowed = false;
                speed = backupSpeed;
                slowBase = 1f / slowDuration;
            }
            slowBase -= Time.deltaTime;
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypointLength - 1)
        {
            Destroy(gameObject);
            Health.currentHealth--;
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            MoneySystem.addCash(enemyValue);
            Destroy(gameObject);
            return;
        }
        else
        {
            currentHealth -= damage;
            scaleChange = new Vector3(3f * (currentHealth / maxHealth), 0.7f, 0f);
            healthBar.localScale = scaleChange;
        }
        
    }

    public void GetSlowed(float slow) // I forgot this was supposed to be like, a decimal, when I implemented it which meant my enemies entered HYPER SPEED
    {
        if (!slowed)
        {
            slowed = true;
            speed *= slow;
        }
    }
}
