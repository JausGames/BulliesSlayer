using UnityEngine;

public class GrenadeThrower : Weapon
{
    [Header("Property")]
    // Damage per grenade
    [SerializeField] private float damage = 15f;
    // grenade speed
    [SerializeField] private float speed = 45f;
    // Range = only for raycast
    [SerializeField] private float range = 100f;
    // Fire Rate 1 =  2.2 shot/second
    private float fireRate = 0.4f;
    private float nextFire = 0f;
    private Vector3 basePos;
    private Quaternion baseRot;

    private float dispertion = 0.25f;

    const int CAPACITY = 2;
    private int capacity = 2;

    [Header("Component")]
    public Camera fpsCam;
    public ParticleSystem flash;
    public GameObject grenade;
    public Transform canon;
    public Transform direction;
    public Animator animator;


    public AudioClip shot; // Drag & Drop the audio clip in the inspector
    public AudioClip reload; // Drag & Drop the audio clip in the inspector
    private AudioSource audioSource;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        basePos = transform.localPosition;
        baseRot = transform.localRotation;

        flash.Stop();
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
        animator.SetTrigger("Reload");
        //Time.timeScale = 0.1f;
        //canShot = false;
    }
    public override void Reload()
    {
        capacity = CAPACITY;
        PlayReloadSound();
        //canShot = true;
    }
    public override void GetInHand()
    {
        transform.localPosition = basePos;
        transform.localRotation = baseRot;
    }
    public override void Trigger()
    {
        if (Time.time <= nextFire || capacity <= 0 || !canShot) return;
        nextFire = Time.time + fireRate;
        animator.SetTrigger("Shoot");
        capacity--;
    }

    override public void Fire()
    {
        PlayShotSound();
        flash.Play();
        RaycastHit hit;
        

            GameObject bul = Instantiate(grenade, canon.position, canon.rotation);
            Vector3 point = direction.position;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                point = hit.point;

            }
            Debug.DrawLine(point, point + Vector3.up, Color.blue, 5f);
        bul.GetComponent<Rigidbody>().AddForce((point - canon.position).normalized * speed, ForceMode.Impulse);      
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
}
