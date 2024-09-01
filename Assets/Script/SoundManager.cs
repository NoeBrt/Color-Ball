using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource soundSource; // Renamed for clarity

    [SerializeField]
    private AudioClip collectSound;

    [SerializeField]
    private AudioClip finishSound;

    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private AudioClip colorChangeSound;

    void Start()
    {
        // Subscribe to actions
        PlayerActions.Collect += OnCollectTriggered;
        PlayerActions.Finish += OnFinishTriggered;
        PlayerActions.Death += OnDeathTriggered;
        PlayerActions.ChangeColor += OnChangeColorTriggered;
    }

    void OnDestroy()
    {
        // Unsubscribe from actions
        PlayerActions.Collect -= OnCollectTriggered;
        PlayerActions.Finish -= OnFinishTriggered;
        PlayerActions.Death -= OnDeathTriggered;
        PlayerActions.ChangeColor -= OnChangeColorTriggered;
    }

    void OnCollectTriggered(int amount)
    {
        PlaySound(collectSound);
    }

    void OnFinishTriggered()
    {
        PlaySound(finishSound);
    }
    void OnChangeColorTriggered()
    {
        PlaySound(colorChangeSound);
    }

    void OnDeathTriggered()
    {
        PlaySound(deathSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (soundSource != null && clip != null)
        {
            soundSource.PlayOneShot(clip);
        }
    }
}
