using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knive : Weapon
{
    [Header("Property")]
    // Damage per cut
    [SerializeField] private float damage = 1000f;
    // Bullet speed
    [SerializeField] private float speed = 300f;
    // Range = only for raycast
    [SerializeField] private float range = 100f;
    // Fire Rate 0.25 = 4 shot/second
    [SerializeField] private float fireRate = 0.25f;
    private float nextFire = 0f;
    private Vector3 basePos;
    private Quaternion baseRot;

    private Vector3 originPos;
    private Quaternion originRot;


    [Header("Component")]
    public ParticleSystem speedParticles;
    public TrailRenderer trail;
    public Animator animator;
    public PlayerCombat combat;
    public KniveCollliderEvent col;
    [SerializeField]  private bool isCutting = false;
    [SerializeField] private bool isThrown = false;
    [SerializeField] private bool planted = false;


    public AudioClip hit; // Drag & Drop the audio clip in the inspector
    public AudioClip attack; // Drag & Drop the audio clip in the inspector
    private AudioSource audioSource;




    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        originPos = transform.localPosition;
        originRot = transform.localRotation;

        animator = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody>();
        speedParticles.Stop();
        col = GetComponentInChildren<KniveCollliderEvent>();

    }
    private void Update()
    {
        trail.enabled = isCutting;
    }
    public void SetPlanted(bool value)
    {
        planted = value;
    }
    public bool GetPlanted()
    {
        return planted;
    }
    public override void GetInHand()
    {
        transform.localPosition = originPos;
        transform.localRotation = originRot;
        body.useGravity = false;
        body.isKinematic = true;
        isThrown = false;
        planted = false;
    }
    public void SetPosRot()
    {
        body.velocity = Vector3.zero;
        basePos = transform.localPosition;
        baseRot = transform.localRotation;
        body.isKinematic = true;
    }
    public bool GetIsCutting()
    {
        return isCutting;
    }
    public void SetIsCutting(bool value)
    {
        isCutting = value;
    }

    public bool GetIsThrown()
    {
        return isThrown;
    }
    public float GetDamage()
    {
        return damage;
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.01f);
    }
    public void SetGravity(bool value)
    {
        body.useGravity = value;
    }
    public override void Trigger()
    {
        if (Time.time <= nextFire || !canShot) return;
        nextFire = Time.time + fireRate;
        animator.SetTrigger("Cut");
        PlayAttackSound();
        speedParticles.Play();
        isCutting = true;
    }

    override public void Fire()
    {
        isCutting = false;
        col.ResetList();
        


    }
    public override void StartReload()
    {
        if (canShot == true)
        {
            animator.SetTrigger("Throw");
        }
    }
    public override void Reload()
    {
        if (GetComponentInParent<Camera>().GetComponentInParent<PlayerCombat>() == null) return;
        combat = GetComponentInParent<Camera>().GetComponentInParent<PlayerCombat>();
        body.useGravity = true;
        body.isKinematic = false;
        transform.SetParent(null);
        body.AddForce((transform.forward*2f + transform.up*0.2f)*speed);
        combat.RemoveWeapon(this);
        isCutting = true;
        isThrown = true;
    }
    
    private AudioSource AudioSource
    {
        get
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            return audioSource;
        }
    }

    public void PlayHitSound()
    {
        AudioSource.PlayOneShot(hit);
    }

    public void PlayAttackSound()
    {
        //AudioSource.PlayOneShot(attack);
    }
}
