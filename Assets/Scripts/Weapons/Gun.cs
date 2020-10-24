using UnityEngine;

public class Gun : Weapon
{
    [Header("Property")]
    // Damage per bullet
    [SerializeField] private float damage = 25f;
    // Bullet speed
    [SerializeField] private float speed = 100f;
    // Range = only for raycast
    [SerializeField] private float range = 100f;
    // Fire Rate 0.25 = 4 shot/second
    [SerializeField] private float fireRate = 0.25f;
    private float nextFire = 0f;
    private Vector3 basePos;
    private Quaternion baseRot;

    const int CAPACITY = 12;
    private int capacity = 12;


    [Header("Component")]
    public Camera fpsCam;
    public ParticleSystem flash;
    public GameObject bullet;
    public Transform canon;
    public Transform direction;
    public Animator animator;


    public AudioClip shot; // Drag & Drop the audio clip in the inspector
    public AudioClip reload; // Drag & Drop the audio clip in the inspector
    private AudioSource audioSource;
    private void Awake()
    {
        basePos = transform.localPosition;
        baseRot = transform.localRotation;
        body = GetComponent<Rigidbody>();

        flash.Stop();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(canon.position, 0.01f);
        Gizmos.DrawWireSphere(direction.position, 0.01f);
        Gizmos.DrawLine(canon.position, direction.position);
    }
    public override void Trigger()
    {
        if (Time.time <= nextFire || capacity <= 0 || !canShot) return;
        capacity--;
        nextFire = Time.time + fireRate;
        animator.SetTrigger("Shoot");
    }

    override public void Fire()
    {
        PlayShotSound();
        flash.Play();

        RaycastHit hit;
        GameObject bul = Instantiate(bullet, canon.position, canon.rotation);
        Vector3 point = direction.position;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            point = hit.point;
        }

        Debug.DrawLine(point, point + Vector3.up, Color.blue, 5f);
        bul.GetComponent<Bullet>().SetDamage(damage);
        bul.GetComponent<Bullet>().SetSpeed(speed);
        bul.GetComponent<Bullet>().SetDirection((point - canon.position).normalized);


    }
    public override void StartReload()
    {
        animator.SetTrigger("Reload");
        canShot = false;
    }
    public override void Reload()
    {
        PlayReloadSound();
        capacity = CAPACITY;
        canShot = true;
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

    public void PlayShotSound()
    {
        AudioSource.PlayOneShot(shot);
     }

    public void PlayReloadSound()
    {
        AudioSource.PlayOneShot(reload);
    }
    public override void GetInHand()
    {
        transform.localPosition = basePos;
        transform.localRotation = baseRot;
    }
}
