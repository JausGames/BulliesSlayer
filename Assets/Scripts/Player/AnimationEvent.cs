using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    public void PlayShot()
    {
        Debug.Log("PlayShot");
        weapon.Fire();
    }
    public void PlayReload()
    {
        weapon.Reload();
    }
}
