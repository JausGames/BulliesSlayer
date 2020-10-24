using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float CoolDown;
    [SerializeField] private List<Rigidbody> bodies;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Transform> enemyPoints;
    [SerializeField] private ParticleSystem blowingParticle;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private bool hasBlown = false;
    [SerializeField] private GameObject visual;
    // Start is called before the first frame update
    void Awake()
    {
        blowingParticle.Stop();
        CoolDown = Time.time + 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBlown) return;
        if (Time.time > CoolDown)
        {
            blowingParticle.Play();
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
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
                var blood = Instantiate(bloodParticle, enem.transform);
                blood.transform.position = enem.GetComponent<Enemy>().belly.position;
                enem.TakeDamage(500f);
            }
            foreach (Rigidbody bod in bodies)
            {
                bod.AddExplosionForce(25f, transform.position, 4f, 2f, ForceMode.Impulse);
            }
            visual.SetActive(false);
            hasBlown = true;
            blowingParticle.Play();
            Destroy(gameObject, 2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
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
