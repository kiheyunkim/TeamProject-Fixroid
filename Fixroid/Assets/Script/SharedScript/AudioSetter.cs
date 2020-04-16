using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * [0] Master
 * [1] Effect
 * [2] BGM
 */
public class AudioSetter : MonoBehaviour
{
    static public AudioSource SetEffect(GameObject where, string path)
    {
        UnityEngine.Audio.AudioMixer audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/SoundMixer");
        if (audioMixer == null) return null;

        AudioClip audioClip = Resources.Load<AudioClip>(path);
        if (audioClip == null) return null;

        AudioSource audioSource = where.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[1];

        return audioSource;
    }

    static public AudioSource SetBgm(GameObject where, string path)
    {
        UnityEngine.Audio.AudioMixer audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/SoundMixer");
        if (audioMixer == null) return null;

        AudioClip audioClip = Resources.Load<AudioClip>(path);
        if (audioClip == null) return null;

        AudioSource audioSource = where.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];
        audioSource.loop = true;

        return audioSource;
    }

    static public void SetBGMVolume(SettingSaveTile settingSaveTile)
    {//Value mapping 0~10 to -80 ~ 0 (mute)
        
        UnityEngine.Audio.AudioMixer audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/SoundMixer");
        audioMixer.SetFloat("BgmVolume", settingSaveTile.bgm ? 0.0f : -80.0f);
    }

    static public void SetEffectVolume(SettingSaveTile settingSaveTile)
    {//Value mapping 0~10 to -80 ~ 0(mute)
        
        UnityEngine.Audio.AudioMixer audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/SoundMixer");
        audioMixer.SetFloat("EffectVolume", settingSaveTile.effect ? 0.0f : -80.0f);
    }

    static public bool GetBGMState()
    {//Value mapping 0~10 to -80 ~ 0 (mute)

        float retval;
        UnityEngine.Audio.AudioMixer audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/SoundMixer");
        audioMixer.GetFloat("BgmVolume", out retval);

        return retval == 0.0f ? false : true;
    }

    static public bool GetEffectState()
    {//Value mapping 0~10 to -80 ~ 0(mute)

        float retval;
        UnityEngine.Audio.AudioMixer audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Sound/SoundMixer");
        audioMixer.GetFloat("EffectVolume", out retval);

        return retval == 0.0f ? false : true;
    }
}
