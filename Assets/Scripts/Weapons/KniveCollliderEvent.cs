using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KniveCollliderEvent : MonoBehaviour
{
    private Knive knive;
    public GameObject blood;
    [SerializeField] private List<Enemy> touched;

    private bool FindEnemy(Enemy enemy)
    {
        if (touched == null) return false;
        foreach (Enemy p in touched)
        {
            if (p == enemy) return true;
        }
        return false;
    }
    private void Awake()
    {
        knive = GetComponentInParent<Knive>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (knive.GetPlanted()) return;
        if (knive.GetIsCutting() && other.GetComponentInParent<Enemy>()!= null && !FindEnemy(other.GetComponentInParent<Enemy>()))
        {
            knive.PlayHitSound();
            Debug.Log("onTriggerEnter : " + other.GetComponentInParent<Enemy>()+ " " + knive.GetDamage());
            other.GetComponentInParent<Enemy>().TakeDamage(knive.GetDamage());
            GameObject bld = Instantiate(blood, GetComponentInParent<Transform>().position, GetComponentInParent<Transform>().rotation, other.transform);
            touched.Add(other.GetComponentInParent<Enemy>());
        }

        if (knive.GetIsThrown())
        {
            knive.SetIsCutting(false);
            knive.transform.SetParent(other.transform);
            if (other.transform.GetComponentInParent<Enemy>() != null)
            {
                var trans = GetComponentInParent<Transform>();
               GameObject bld = Instantiate(blood, trans.position + trans.forward * 0.5f, trans.rotation, other.transform);
            }
            knive.SetPosRot();
            knive.SetGravity(false);
            knive.SetPlanted(true);
        }
    }
    public void ResetList()
    {
        touched.Clear();
    }
}
