using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSystem : MonoBehaviour
{
    public Transform target;
    [Header("Attributes")] //These things are stuff that will get changed on a tower to tower basis, as well as for upgrades and such
    public float towerRadius = 10f;
    public float fireRate = 1f;
    public float slow = 0.6f;
    public float damage = 20f;
    public int upgradeLevel = 0;
    private float shotsPerSecond = 0f;
    private bool[] towerType;
    private float[] towerVals;
    [Header("Unity requirements")]
    public string enemyTag = "Enemy";
    public GameObject projectilePrefab;
    public Transform projectileSpawn;


    private Enemy enemyTarget;

    TowerSpawners towerSpawner;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
        towerType = new bool[5]; // 0 = damage, 1 = slow, 2 = damage over time(poison, fire, etc) (not implemented), 3 = splash damage (not implemented), 4 = reversal (not implemented)
        towerVals = new float[5]; // see above
        if (damage > 0f)
            towerType[0] = true;
        if (slow > 0f)
            towerType[1] = true;
        towerType[2] = false;
        towerType[3] = false;
        towerType[4] = false;
        if (towerType[0])
            towerVals[0] = damage;
        if (towerType[1])
            towerVals[1] = slow;
        //the other 3 aren't implemented, so I am not going to bother right now
        towerSpawner = TowerSpawners.instance;
    }
    private void Update()
    {
        if (target == null)
            return;

        if (shotsPerSecond <= 0f)
        {
            Shoot();
            shotsPerSecond = 1f / fireRate;
        }
        shotsPerSecond -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        Projectiles projectile = projectileGO.GetComponent<Projectiles>();
        //upgradeTower();
        if (projectile != null) 
            projectile.getTarget(target, enemyTarget, towerType, towerVals);
    }

    void updateTarget()
    {
        float nearestEnemy = Mathf.Infinity;
        GameObject currentTarget = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance between the two objects
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= nearestEnemy)
            {
                nearestEnemy = distance;
                currentTarget = enemy;
                enemyTarget = enemy.GetComponent<Enemy>();
            }
        }
        if (currentTarget != null && nearestEnemy <= towerRadius)
        {
            target = currentTarget.transform;
        }
        else
        {
            target = null;
        }
    }

    public void upgradeTower()
    {
        upgradeLevel += 1;
        if (upgradeLevel%5  == 0)
        {
            fireRate += 0.5f;
        }
        if (towerType[0])
        {
            damage += upgradeLevel * 1.3f * 10f;
            towerVals[0] = damage;
        }
        if (towerType[1])
        {
            slow -= upgradeLevel * 0.1f;
            towerVals[1] = slow;
        }
    }

    public void sellTower()
    {
        MoneySystem.addCash(50);
        Destroy(gameObject);
        return;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, towerRadius); // When a tower is selected, it will display a gray sphere around itself that shows the range
    }

    void OnMouseDown()
    {
        Debug.Log("selected");
        towerSpawner.SelectBuiltTower(this);
    }
}
