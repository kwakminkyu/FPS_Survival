using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource[] audioSourcesEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlaySE(string name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (name == effectSounds[i].name)
            {
                for(int j = 0; j < audioSourcesEffects.Length; j++)
                {
                    if (!audioSourcesEffects[j].isPlaying)
                    {
                        playSoundName[i] = effectSounds[i].name;
                        audioSourcesEffects[j].clip = effectSounds[i].clip;
                        audioSourcesEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오소스 사용중");
                return;
            }
        }
        Debug.Log(name + "사운드가 SoundManager에 등록되지 않았습니다");
    }

    private void Start()
    {
        playSoundName = new string[audioSourcesEffects.Length];
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }

    public void StopSE(string name)
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            if (playSoundName[i] == name)
            {
                audioSourcesEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + name + "사운드가 없습니다");
    }
}
