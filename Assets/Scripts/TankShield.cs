using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    private Coroutine _shieldRoutine;
    
    public void ActivateShield(float duration)
    {
        if (_shieldRoutine != null)
        {
            StopCoroutine(_shieldRoutine);
        }
        _shieldRoutine = StartCoroutine(ShieldRoutine(duration));
    }
 
    private IEnumerator ShieldRoutine(float duration)
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(duration);
        shield.SetActive(false);
        _shieldRoutine = null;
    }
}
