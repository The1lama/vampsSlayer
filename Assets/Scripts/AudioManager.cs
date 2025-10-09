using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance { get; private set; }
   
   [Header("Audio Mixer")]
   [SerializeField] private AudioMixer audioMixer;

   [Header("Mixer Group Ref")] 
   [SerializeField] private AudioMixerGroup masterGroup;
   [SerializeField] private AudioMixerGroup backgroundGroup;
   [SerializeField] private AudioMixerGroup walkingGroup;
   [SerializeField] private AudioMixerGroup sfxGroup;
   
   private Dictionary<MixerGroup, AudioMixerGroup> _mixerGroupMap;

   [SerializeField] private GameObject audioSourcePrefab;
   
   private void InitializeMixerGroups()
   {
      _mixerGroupMap = new Dictionary<MixerGroup, AudioMixerGroup>
      {
         { MixerGroup.Master, masterGroup },
         { MixerGroup.Background, backgroundGroup },
         { MixerGroup.SFX, sfxGroup },
         { MixerGroup.Walking, walkingGroup }
      };
   }
   
   private void Awake()
   {
      // if there are more game managers in the scene this game manager gets removed
      if (Instance != null && Instance != this)
      {
         Destroy(this);
      }
      else
      {
         Instance = this;
         InitializeMixerGroups();
      }
   }

   public void PlaySound(AudioClip clip, MixerGroup mixerGroup, Transform parrentTransform = null, float volume = 1f)
   {
      if (clip == null)
      {
         Debug.LogWarning("Playsound: Audio clip is null");
         return;
      }

      if (!_mixerGroupMap.TryGetValue(mixerGroup, out var group) || group == null)
      {
         Debug.LogWarning($"Playsound: Audio group {mixerGroup} not found");
         return;
      }


      AudioSource source;

      GameObject tempGO;
      if (audioSourcePrefab != null)
      {
         tempGO = Instantiate(audioSourcePrefab);
      }
      else
      {
         tempGO = new GameObject($"TempAudio_{clip.name}");
         source = tempGO.AddComponent<AudioSource>();
      }

      // Set parent if provided
      if (parrentTransform != null)
      {
         tempGO.transform.SetParent(parrentTransform);
      }

      source = tempGO.GetComponent<AudioSource>();
      if (source == null) source = tempGO.AddComponent<AudioSource>();

      source.clip = clip;
      source.outputAudioMixerGroup = group;
      // source.volume = volume;
      source.Play();
      
      // ObjectPoolManager.ReturnObjectToPool(tempSound);

   }


  
   
   
}



public enum MixerGroup
{
   Master,
   Background,
   SFX,
   Walking
}