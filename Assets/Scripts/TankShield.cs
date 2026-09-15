using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    private Coroutine _shieldRoutine;
    [SerializeField] private GameObject ui;
    [SerializeField] private Image fillUi;
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
        ui.SetActive(true);
        fillUi.fillAmount = 0f;
 
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fillUi.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
 
        shield.SetActive(false);
        ui.SetActive(false);
        _shieldRoutine = null;
    }
}
