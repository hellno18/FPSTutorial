using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SE : MonoBehaviour
{
    [SerializeField] private AudioClip m_ButtonSFX;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

    }

    public void PlayButtonSFX()
    {
        audioSource.PlayOneShot(m_ButtonSFX);
    }
}
