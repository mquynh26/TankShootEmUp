using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretChange : MonoBehaviour
{
    [SerializeField] private GameObject[] turrets;
    [SerializeField] private int startingIndex;
 
    private int _activeIndex;
 
    private void Awake()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].SetActive(i == startingIndex);
        }
 
        _activeIndex = startingIndex;
    }
 
    public void SwitchTo(int index)
    {
        if (index < 0 || index >= turrets.Length || index == _activeIndex)
        {
            return;
        }
 
        turrets[_activeIndex].SetActive(false);
        turrets[index].SetActive(true);
        _activeIndex = index;
    }
    
    public WeaponShoot GetActiveWeaponShoot()
    {
        return turrets[_activeIndex].GetComponent<WeaponShoot>();
    }
}
