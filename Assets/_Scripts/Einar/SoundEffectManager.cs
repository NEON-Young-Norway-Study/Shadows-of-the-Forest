using System.Collections;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{

    [SerializeField] private AudioSource soundFXObject;
    bool mute = true;
    public static SoundEffectManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(1f);
        mute = false;
    }
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawmTransform, float volume, float duration = 0.0f)
    {
        if (mute)
        {
            return;
        }
        //spawn in GameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawmTransform.position, Quaternion.identity);
        //assign audioClip
        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after playing
        if (duration > 0.0f)
        {
            Destroy(audioSource.gameObject, duration);
        }
        else
        {
            Destroy(audioSource.gameObject, clipLength);
        }
    }
    //[SerializeField] private AudioClip SFXname;
    //SoundEffectManager.Instance.PlaySoundFXClip(SFXname, transform, 1f);
}
