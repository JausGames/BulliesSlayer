using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocktailMolotov : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> bodies;

    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Transform> enemyPoints;
    [SerializeField] private ParticleSystem blowingParticle;
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject burnParticle;
    [SerializeField] private bool hasBlown = false;
    [SerializeField] private GameObject visual;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private float cooldown = 5f;
    private float nextFire = 0f;
    
    // Start is called before the first frame update
    void Awake()
    {
        blowingParticle.Stop();
    }
    private void FixedUpdate()
    {
        if (hasBlown && Time.time >= nextFire)
        {
            
            nextFire = Time.time + fireRate;
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
                Debug.Log("Kill with cocktail molotov");
                var blood = Instantiate(bloodParticle, enem.transform);
                blood.transform.position = enem.GetComponent<Enemy>().belly.position;
                enem.TakeDamage(5f);
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasBlown) return;
        blowingParticle.Play();
        
        
        
        visual.SetActive(false);
        hasBlown = true;
        blowingParticle.Play();
        Destroy(gameObject, cooldown);
    
}
    // Update is called once per frame
    

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
