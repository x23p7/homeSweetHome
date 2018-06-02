using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public float fadeTimeInSeconds;
    public AudioSource[] audioSources;

    bool fadingIn;
    bool fadingOut;
    float maxVol;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void PlayClip(string clipName, bool fadeIn)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.transform.name.ToLowerInvariant() == clipName.ToLowerInvariant())
            {
                if (fadeIn)
                {
                    StartCoroutine(FadeIn(audioSource));
                }
                else
                {
                    audioSource.Play();
                }
            }
        }
    }

    public void StopClip(string clipName, bool fadeOut)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.transform.name.ToLowerInvariant() == clipName.ToLowerInvariant())
            {
                if (fadeOut)
                {
                    StartCoroutine(FadeOut(audioSource));
                }
                else
                {
                    audioSource.Stop();
                }
            }
        }
    }

    public void StopAll()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    IEnumerator FadeIn(AudioSource audioSource)
    {
        while (fadingOut)
        {
            yield return null;
        }
        fadingIn = true;
        maxVol = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();
        while (audioSource.volume < maxVol)
        {
            print("fadingIn");
            audioSource.volume += maxVol*(1 / fadeTimeInSeconds) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (audioSource.volume > maxVol)
            {
                audioSource.volume = maxVol;
            }
        }
        fadingIn = false;
    }
    IEnumerator FadeOut(AudioSource audioSource)
    {
        while (fadingIn)
        {
            yield return null;
        }
        fadingOut = true;
        while (audioSource.volume > 0)
        {
            print("fadingOut");
            audioSource.volume -= maxVol* (1 / fadeTimeInSeconds) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (audioSource.volume < 0)
            {
                audioSource.volume = 0;
            }
        }
        audioSource.Stop();
        audioSource.volume = maxVol;
        fadingOut = false;
    }
}
