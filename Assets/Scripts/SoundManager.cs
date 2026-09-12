using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip playerShootClip;
    [SerializeField] private AudioClip playerHitClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip pickBuffClip;
    [SerializeField] private AudioClip enemyShootClip;
    [SerializeField] private AudioClip enemyShootClip1;
    [SerializeField] private AudioClip mineClip;
    [SerializeField] private AudioClip enemyDeathClip;
    [SerializeField] private AudioClip buildingClip;
    [SerializeField] private AudioClip treeClip;
    [SerializeField] private AudioClip overClip;
    [SerializeField] private AudioClip countDownClip;

    public bool isMute = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void ToggleMute()
    {
        isMute = !isMute;
        sfxSource.mute = isMute;
    }
 
    public void PlayPlayerShoot() => PlaySfx(playerShootClip);
    public void PlayPlayerHit() => PlaySfx(playerHitClip);
    public void PlayTree() => PlaySfx(treeClip);
    public void PlayBuilding() => PlaySfx(buildingClip);
    public void PlayEnemyShoot() => PlaySfx(enemyShootClip);
    public void PlayEnemyShoot1() => PlaySfx(enemyShootClip1);
    public void PlayEnemyDeath() => PlaySfx(enemyDeathClip);
    public void PlayHit() => PlaySfx(hitClip);
    public void PlayBuffPickup() => PlaySfx(pickBuffClip);
    public void PlayMine() => PlaySfx(mineClip);
    public void PlayGameOver() => PlaySfx(overClip);
    public void PlayCountDown() => PlaySfx(countDownClip);
    
 
    private void PlaySfx(AudioClip clip)
    {
        if (clip == null || sfxSource == null || isMute)
        {
            return;
        }
 
        sfxSource.PlayOneShot(clip);
    }
}
