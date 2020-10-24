using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Property")]
    [SerializeField] Vector3 point;
        public LayerMask weaponLayer;

    [Header("Component")]
    public Camera fpsCam;
    public List<Weapon> weapons = new List<Weapon>();
    public Weapon equipedWeapon;


    private void Awake()
    {
        foreach (Weapon wp in weapons)
        {
            if (wp != null)
            {
                if (equipedWeapon != null) wp.gameObject.SetActive(false);
                else equipedWeapon = wp;
            }
        }
    }
    public void Shoot(bool perf, bool canc)
    {
        if (perf) equipedWeapon.Trigger();
    }
    public void Reload(bool perf, bool canc)
    {
        if (perf) equipedWeapon.StartReload();
    }
    public void ChangeWeapon(bool perf, bool canc)
    {
        if (perf) ChangeWeapon();
    }
    public void Action(bool perf, bool canc)
    {
        if (perf) PickUp();
    }
    public void Throw(bool perf, bool canc)
    {
        if (perf) Throw();
    }

    void ChangeWeapon()
    {
        var max = weapons.Count;
        var num = FindWeapon(equipedWeapon);
        Debug.Log("FindWeapon, count = " + max);
        Debug.Log("FindWeapon, num = " + num);
        weapons[FindWeapon(equipedWeapon)].gameObject.SetActive(false);
        if (num == max - 1)
        {
            equipedWeapon = weapons[0];
            weapons[0].gameObject.SetActive(true);
    }
        else
        {
            Debug.Log("FindWeapon2, num = " + num);
            Debug.Log("weapon = " + weapons[num++]);
            equipedWeapon = weapons[num++];
        }
        weapons[FindWeapon(equipedWeapon)].gameObject.SetActive(true);
    }
    public void PickUp()
    {
        RaycastHit hit;
        point = transform.position + transform.forward * 2f;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 4f))
        {
            Debug.Log(hit.transform.name);
            point = hit.point;
        }
        var colliders = Physics.OverlapSphere(point, 0.5f,weaponLayer);
        if (colliders.Length > 0)
        {
            var validated = false;
            for (var i = 0; i < colliders.Length || !validated ; i++)
            {
                if (FindWeapon(colliders[i].GetComponentInParent<Weapon>()) > -1) return;
                weapons.Add(colliders[i].GetComponentInParent<Weapon>());
                Debug.Log("PickUp, weapon = " + colliders[i].GetComponentInParent<Weapon>());
                equipedWeapon.gameObject.SetActive(false);
                equipedWeapon = weapons[weapons.Count - 1];
                validated = true;
                equipedWeapon.transform.SetParent(fpsCam.transform);
                equipedWeapon.GetInHand();
                equipedWeapon.GetRigidbody().isKinematic = true;
                equipedWeapon.GetRigidbody().useGravity = false;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(point, 0.5f);
    }
    public void RemoveWeapon(Weapon weapon)
    {
        var max = weapons.Count;
        var num = FindWeapon(weapon);
        //weapons[num] = null;
        weapons.RemoveAt(num);
        if (max == 1) equipedWeapon = null;
        if (num == max - 1)
        {
            equipedWeapon = weapons[0];
            weapons[0].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("FindWeapon2, num = " + num);
            Debug.Log("weapon = " + weapons[num]);
            equipedWeapon = weapons[num];
        }
        weapons[FindWeapon(equipedWeapon)].gameObject.SetActive(true);

    }
    void Throw()
    {
        equipedWeapon.GetRigidbody().isKinematic = false;
        equipedWeapon.GetRigidbody().useGravity = true;
        equipedWeapon.transform.SetParent(null);
        RemoveWeapon(equipedWeapon);
    }
    private int FindWeapon(Weapon weapon)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == weapon) return i;
        }
        return -1;
    }
}
