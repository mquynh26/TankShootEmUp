using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDetect : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.ReturnPool();
        }
    }
}
