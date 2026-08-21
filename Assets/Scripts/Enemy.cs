using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform killPoint;
    
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.y - killPoint.position.y) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
