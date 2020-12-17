using UnityEngine;
using UnityEngine.Audio;

namespace GameBase
{
    public class SoundFX : MonoBehaviour
    {
        public static SoundFX instance;
        public AudioMixer masterMixer;

        [Header("Audio Sources")]
        public AudioSource[] audioSources;

        private void Start() {
            instance = this;
            masterMixer.SetFloat("sfx", Mathf.Log(GameMaster.GetSO<GameDataSO>().soundfxMaxVolume) * 20);
        }

        public void changeMaxVolume(float volume) {
            masterMixer.SetFloat("sfx", Mathf.Log(volume) * 20);
        }

        public void playSoundFx(AudioClip clip) {
            foreach (var audioSource in audioSources) {
                if (audioSource.isPlaying == false) {
                    audioSource.clip = clip;
                    audioSource.Play();
                    break;
                }
            }
        }

    }
}
