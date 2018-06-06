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
    float[] maxVol;

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

    private void Start()
    {
        maxVol = new float[audioSources.Length];
        for (int i = audioSources.Length; i > 0; i--)
        {
            maxVol[i - 1] = audioSources[i - 1].volume;
            audioSources[i - 1].volume = 0;
        }
    }

    public void PlayClip(string clipName, bool fadeIn)
    {
        for (int i = audioSources.Length; i > 0; i--)
        {
            if (audioSources[i - 1].transform.name.ToLowerInvariant() == clipName.ToLowerInvariant())
            {
                if (fadeIn)
                {
                    if (!audioSources[i - 1].isPlaying)
                    {
                        audioSources[i - 1].Play();
                    }
                    StartCoroutine(FadeIn(audioSources[i - 1], maxVol[i - 1]));
                }
                else
                {
                    audioSources[i - 1].Play();
                }
            }
        }
    }
    public void FadeUp(string clipName, float targetVolume)
    {
        for (int i = audioSources.Length; i > 0; i--)
        {
            if (audioSources[i - 1].transform.name.ToLowerInvariant() == clipName.ToLowerInvariant())
            {
                StartCoroutine(FadeIn(audioSources[i - 1], targetVolume));
            }
        }
    }

    public void StopClip(string clipName, bool fadeOut)
    {
        for (int i = audioSources.Length; i > 0; i--)
        {
            if (audioSources[i - 1].transform.name.ToLowerInvariant() == clipName.ToLowerInvariant())
            {
                if (fadeOut)
                {
                    StartCoroutine(FadeOut(audioSources[i - 1], maxVol[i - 1]));
                }
                else
                {
                    audioSources[i - 1].Stop();
                }
            }
        }
    }

    public void FadeDown(string clipName, float targetVolume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.transform.name.ToLowerInvariant() == clipName.ToLowerInvariant())
            {
                StartCoroutine(FadeOut(audioSource, targetVolume));
            }
        }
    }

    public void StartAll()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Play();
        }
    }
    public void StopAll()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    IEnumerator FadeIn(AudioSource audioSource, float targetVolume)
    {
        while (fadingOut)
        {
            yield return null;
        }
        fadingIn = true;
        audioSource.volume = 0;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * (1 / fadeTimeInSeconds) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (audioSource.volume > targetVolume)
            {
                audioSource.volume = targetVolume;
            }
        }
        fadingIn = false;
        yield return new WaitForFixedUpdate();
    }
    IEnumerator FadeOut(AudioSource audioSource, float targetVolume)
    {
        while (fadingIn)
        {
            yield return null;
        }
        fadingOut = true;
        while (audioSource.volume > 0)
        {
            audioSource.volume -=  targetVolume* (1 / fadeTimeInSeconds) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (audioSource.volume < 0)
            {
                audioSource.volume = 0;
            }
        }
        fadingOut = false;
        yield return new WaitForFixedUpdate();
    }
}
