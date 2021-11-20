using UnityEngine;
using System.Linq;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.Utilities;

public class GameManager : Singleton<GameManager>
{
    private AudioSource audioSource;

    [SerializeField]
    public AbstractMap map;

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

    public void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();

        map.OnTileFinished += (UnityTile tile) => Debug.Log("tile finished");
    }

    public void PlaySound(SoundEffectTypes sfx)
    {
        if (audioSource != null)
        {
            var clip = SoundEffects.Where<SoundEffect>(x => x.Key == sfx).FirstOrDefault();

            if (clip != null)
            {
                audioSource.PlayOneShot(clip.Sound);
            }
        }
    }
}
