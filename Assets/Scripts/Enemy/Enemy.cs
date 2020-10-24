using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public JointFollowAnimRot[] joints = null;
    public Rigidbody spine = null;
    public EnemyCircleMovement movement = null;
    public Transform belly;
    const float RESET_SOUND = 0.05f;
    private float resetSoundSource;

    public AudioClip[] hitSound; // Drag & Drop the audio clip in the inspector
    private AudioSource audioSource;

    private void Start()
    {
        joints = GetComponentsInChildren<JointFollowAnimRot>();
    }
    public void TakeDamage (float amount)
    {
        health -= amount;
        PlayHitSound();
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        foreach (JointFollowAnimRot joint in joints)
        {
            joint.enabled = false;
        }
        spine.constraints = RigidbodyConstraints.None;
        movement.enabled = false;
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
        if (Time.time <= resetSoundSource) return;
        var max = hitSound.Length - 1;
        var num = Random.Range(0, max);
        var player = FindObjectOfType<PlayerCombat>().transform;
        var dist = (player.position - transform.position).magnitude;
        Debug.Log("PlayHitSound, max : " + max);
        Debug.Log("PlayHitSound, num : " + num);
        Debug.Log("PlayHitSound, dist : " + dist);
        AudioSource.volume = 100 - Mathf.Clamp(dist * 5f, 1f, 100f);
        AudioSource.PlayOneShot(hitSound[num]);
        resetSoundSource = Time.time + RESET_SOUND;
    }
}
