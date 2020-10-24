using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{

    protected bool canShot = true;
    protected Rigidbody body;
    abstract public void Trigger();
    abstract public void Fire();
    abstract public void StartReload();
    abstract public void Reload();
    public void SetCanShot(bool value)
    {
        canShot = value;
    }
    public Rigidbody GetRigidbody()
    {
        return body;
    }
    abstract public void GetInHand();
}
