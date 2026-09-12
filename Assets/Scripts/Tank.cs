using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform killPoint;
    
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.y - killPoint.position.y) < 0.05f)
        {
            GameManager.Instance.GameOver();
        }
    }
}
