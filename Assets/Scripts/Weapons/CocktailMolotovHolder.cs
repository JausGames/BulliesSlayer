using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CocktailMolotovHolder : Weapon
{

    public GameObject cocktailMolotov;
    // Fire Rate 0.25 = 4 shot/second
    [SerializeField] private float fireRate = 1f;
    private float nextFire = 0f;
    const int CAPACITY = 99;
    private int capacity = 99;

    private Vector3 basePos;
    private Quaternion baseRot;

    public Transform canon;
    public Transform direction;
    public Animator animator;
    public GameObject visual;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        basePos = transform.localPosition;
        baseRot = transform.localRotation;
    }
    private void Update()
    {
        if (Time.time <= nextFire) visual.SetActive(false);
        else visual.SetActive(true);
    }
    public override void GetInHand()
    {
        transform.localPosition = basePos;
        transform.localRotation = baseRot;
    }

    public override void Trigger()
    {
        if (Time.time <= nextFire || !canShot) return;
        nextFire = Time.time + fireRate;
        //animator.SetTrigger("Shoot");
        Fire();
    }
    public override void Fire()
    {
        RaycastHit hit;
        GameObject bul = Instantiate(cocktailMolotov, canon.position, canon.rotation);
        Vector3 point = canon.position;
        /*if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100f))
        {
            Debug.Log(hit.transform.name);
            point = hit.point;
        }*/
        bul.GetComponent<Rigidbody>().AddForce(((point - transform.position + transform.forward + transform.up).normalized + Vector3.up * 0.2f) * 4f, ForceMode.Impulse);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(canon.position, 0.01f);
        Gizmos.DrawWireSphere(direction.position, 0.01f);
        Gizmos.DrawLine(canon.position, direction.position);
    }
    public override void StartReload()
    {
        /*animator.SetTrigger("Reload");
        canShot = false;*/
    }
    public override void Reload()
    {
        /*PlayReloadSound();
        capacity = CAPACITY;
        canShot = true;*/
    }
}
