using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    [Space]
    public AudioClip moveClip;
    public AudioClip jumpClip;
    public AudioClip shootingClip;

    public void PlayMoveClip()
    {
        audioSource.clip = moveClip;
        audioSource.Play();
    }

    public void PlayJumpClip()
    {
        audioSource.clip = jumpClip;
        audioSource.Play();
    }

    public void PlayShootingClip()
    {
        audioSource.PlayOneShot(shootingClip);
    }
}
