using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GameBase
{
    public class Voice : MonoBehaviour {
        public static Voice instance;

        public AudioMixer masterMixer;

        [Header("Audio Sources")]
        public AudioSource audioSource;

        public bool isPlaying = false;
        private IEnumerator coroutine;
        private bool crisRunning = false;

        private void Start() {
            instance = this;
            masterMixer.SetFloat("voice", Mathf.Log(GameMaster.GetSO<GameDataSO>().voiceMaxVolume) * 20);
        }

        /// <summary>
        /// Change voice volume. Method automatically converts input value between 0.01f to 1f to value appropriate for AudioMixer.
        /// </summary>
        /// <param name="volume"></param>
        public void changeMaxVolume(float volume) {
            masterMixer.SetFloat("voice", Mathf.Log(volume) * 20);
        }

        /// <summary>
        /// Play Dialogue clip. Starts / Stops coroutine to reliably report if a dialogue clip is playing or not.
        /// </summary>
        /// <param name="clip"></param>
        public void playDialogue(AudioClip clip) {
            audioSource.clip = clip;
            audioSource.Play();

            if (crisRunning) { 
                StopCoroutine(coroutine);
            }

            coroutine = CheckifIsPlaying(clip.length);
            StartCoroutine(coroutine);   
        }


        IEnumerator CheckifIsPlaying(float mylength) {
            crisRunning = true;
            isPlaying = true;
            yield return new WaitForSeconds(mylength);
            crisRunning = false;
            isPlaying = false;
        }

    }
}
