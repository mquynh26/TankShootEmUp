using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatMove : MonoBehaviour
{
    public static float Speed;
    [SerializeField] private float speed = 3.5f;

    void Awake()
    {
        Speed = speed;
    }
    private void LateUpdate()
    {
        transform.Translate(Vector2.down * (speed * Time.deltaTime), Space.World);
        
    }
}
