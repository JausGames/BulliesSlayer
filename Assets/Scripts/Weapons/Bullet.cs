using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private float speed = 0f;
    private float damage = 0f;
    private float timer = 3f;
    private Rigidbody _rigidbody = null;
    private bool hadContact = false;
    public GameObject blood;
    public GameObject solid;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }
    private void LateUpdate()
    {
        //if(!hadContact) _rigidbody.velocity += dir * speed * Time.deltaTime;
        //if (!hadContact)  _rigidbody.AddForce(Vector3.up * 180f);
        timer -= Time.deltaTime;
        if (timer <= 0f) Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 fdir)
    {
        dir = fdir;
        _rigidbody.AddForce(dir * speed, ForceMode.Impulse);
    }
    public void SetDamage(float dam)
    {
        damage = dam;
    }
    public void SetSpeed(float spe)
    {
        speed = spe;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hadContact) return;
        Debug.Log("OnColisionEnter");
        var col = collision.GetContact(0);
        if (col.otherCollider.gameObject.GetComponent<Bullet>()) return;
        Enemy enemy = col.otherCollider.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            Instantiate(blood, collision.GetContact(0).point, Quaternion.Euler(col.normal));
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(solid, collision.GetContact(0).point, Quaternion.Euler(col.normal));
        }
        Debug.DrawRay(col.point, col.normal, Color.red, 5f);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = true;
        hadContact = true;

        /*if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();*/
    }
}
