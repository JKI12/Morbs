using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    private AudioSource AudioSource;

    public enum SoundEffectTypes
    {
        BEACON
    }

    [System.Serializable]
    public class SoundEffect
    {
        public SoundEffectTypes Key;
        public AudioClip Sound;
    }

    public SoundEffect[] SoundEffects;

    private List<string> ActiveBeacons = new List<string>();

    public void Start()
    {
        AudioSource = FindObjectOfType<AudioSource>();
    }

    public void PlaySound(SoundEffectTypes sfx)
    {
        if (AudioSource != null)
        {
            var clip = SoundEffects.Where<SoundEffect>(x => x.Key == sfx).FirstOrDefault();

            if (clip != null)
            {
                AudioSource.PlayOneShot(clip.Sound);
            }
        }
    }

    public void SaveBeaconToState(string id)
    {
        if (!ActiveBeacons.Contains(id))
        {
            ActiveBeacons.Add(id);
        }
    }

    public bool IsBeaconActive(string id)
    {
        return ActiveBeacons.Contains(id);
    }
}
