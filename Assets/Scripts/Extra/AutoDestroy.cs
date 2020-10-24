using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float destroyTime = 10f;
    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }
}
