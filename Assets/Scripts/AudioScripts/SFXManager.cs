using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject SFXObject;

    public static SFXManager instance;

    private GameObject audioGameObject;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {

        audioGameObject = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        audioSource = audioGameObject.GetComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void StopSFXClip()
    {
        audioSource.Stop();
    }
}
