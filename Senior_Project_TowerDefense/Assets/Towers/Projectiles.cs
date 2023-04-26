using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Transform target;
    public Enemy targetEnemy;
    public float speed = 15f;
    private float damage;
    private bool dmg = false;
    private float slow;
    private bool slw = false; // bools will be restricted to 3 characters here for separation

    public void getTarget(Transform _enemy, Enemy _enemyObject, bool[] towerTypes, float[] towerVals)
    {
        target = _enemy;
        targetEnemy = _enemyObject;
        if (towerTypes[0])
        {
            dmg = true;
            damage = towerVals[0]; // 0 = damage, 1 = slow, 2 = damage over time(poison, fire, etc) (not implemented), 3 = splash damage (not implemented), 4 = reversal (not implemented)
        }// just stole comment from tower ^^^
        if (towerTypes[1])
        {
            slw = true;
            slow = towerVals[1];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 dir = target.position - transform.position;
        float velocity = speed * Time.deltaTime;
        if (dir.magnitude <= velocity)
        {
            targetCollide();
            return;
        }
        transform.Translate(dir.normalized * velocity, Space.World);
    }

    void targetCollide()
    {
        if (dmg)
            targetEnemy.TakeDamage(damage);
        if (slw)
            targetEnemy.GetSlowed(slow);
        Destroy(gameObject);
    }
}
