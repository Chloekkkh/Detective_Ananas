using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Second
{
    [Header("clip")]
    public AudioClip clip;
    [Header("Group")]
    public AudioMixerGroup group;

    [Header("Volume")]
    [Range(0, 1)]
    public float volume = 1;

    [Header("Play on awake")]
    public bool  playOnAwake;

    [Header("Loop")]
    public bool loop;
}
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public List<Second> sounds;
    private Dictionary<string, AudioSource> audioDic;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        audioDic = new Dictionary<string, AudioSource>();
        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.parent = transform;

            AudioSource sourse = obj.AddComponent<AudioSource>();
            sourse.clip = sound.clip;
            sourse.outputAudioMixerGroup = sound.group;
            sourse.volume = sound.volume;
            sourse.playOnAwake = sound.playOnAwake;
            sourse.loop = sound.loop;

            if (sound.playOnAwake)
            {
                sourse.Play();
                if(sourse.clip.name == "Final")
                    DontDestroyOnLoad(instance.gameObject);
            }

            audioDic.Add(sound.clip.name, sourse);
        }
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isWait"></param>

    public static void Play(string name, bool isWait = false)
    {
        if(!instance.audioDic.ContainsKey(name))
        {
            Debug.LogWarning("AudioManager: " + name + "not found!");
            return;
        }
        if(isWait)
        {
            if(!instance.audioDic[name].isPlaying)
                instance.audioDic[name].Play();
        }
        else
        {
            instance.audioDic[name].Play();
        }
    }
    /// <summary>
    /// 停止播放
    /// </summary>
    /// <param name="name"></param>

    public static void Stop(string name)
    {
        if (!instance.audioDic.ContainsKey(name))
        {
            Debug.LogWarning("AudioManager: " + name + "not found!");
            return;
        }
        instance.audioDic[name].Stop();
    }

}
