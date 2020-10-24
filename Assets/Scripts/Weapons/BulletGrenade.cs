using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGrenade : Bullet
{
    [SerializeField] private ParticleSystem blowingParticle;
    [SerializeField] private List<Rigidbody> bodies;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private bool hasBlown = false;
    [SerializeField] private GameObject visual;

    private void Awake()
    {
        blowingParticle.Stop();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter : BulletGrenade");
        blowingParticle.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider col in colliders)
        {
            if (col.GetComponent<Rigidbody>() != null && !FindBody(col.GetComponent<Rigidbody>()) && col.gameObject != this.gameObject) bodies.Add(col.GetComponent<Rigidbody>());
            if (col.GetComponentInParent<Enemy>() != null && !FindEnemy(col.GetComponentInParent<Enemy>()))
            {
                enemies.Add(col.GetComponentInParent<Enemy>());
            }
        }
        foreach (Enemy enem in enemies)
        {
            Debug.Log("Kill with grenade");
            var bld = Instantiate(blood, enem.transform);
            blood.transform.position = enem.GetComponent<Enemy>().belly.position;
            enem.TakeDamage(500f);
        }
        foreach (Rigidbody bod in bodies)
        {
            bod.AddExplosionForce(15f, transform.position, 4f, 1f, ForceMode.Impulse);
        }
        visual.SetActive(false);
        hasBlown = true;
        blowingParticle.Play();
        Destroy(gameObject, 2f);
    }

    private bool FindEnemy(Enemy enemy)
    {
        if (enemies == null) return false;
        foreach (Enemy p in enemies)
        {
            if (p == enemy) return true;
        }
        return false;
    }
    private bool FindBody(Rigidbody body)
    {
        if (bodies == null) return false;
        foreach (Rigidbody p in bodies)
        {
            if (p == body) return true;
        }
        return false;
    }
}
