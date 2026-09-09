using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] renderers;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration = 0.08f;
 
    private Material _originalMaterial;
    private Coroutine _flashRoutine;
 
    private void Awake()
    {
        _originalMaterial = renderers[0].material;
    }
 
    public void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }
 
        _flashRoutine = StartCoroutine(FlashRoutine());
    }
 
    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = flashMaterial;
        }
 
        yield return new WaitForSeconds(flashDuration);
 
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = _originalMaterial;
        }
 
        _flashRoutine = null;
    }
}