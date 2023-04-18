using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public AudioClip crash;
    public AudioClip explosion;
    public AudioClip buttonSelectClip;

    private float crashLength;
    private float crashTimer;

    void Awake()
    {
        crashLength = crash.length;
        crashTimer = 0;
        audioSource.PlayOneShot(buttonSelectClip);
    }

    void Update()
    {
        if (crashTimer > 0)
            crashTimer = Mathf.Max(0, crashTimer - Time.deltaTime);
    }

    public void PlayCrash()
    {
        if (crashTimer == 0)
        {
            audioSource.PlayOneShot(crash);
            crashTimer = crashLength;
        }
    }

    public void PlayExplosion() => audioSource.PlayOneShot(explosion);
}
