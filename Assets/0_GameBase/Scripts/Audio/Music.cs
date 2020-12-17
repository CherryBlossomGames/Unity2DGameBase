using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace GameBase{
    public class Music : MonoBehaviour
    {
        public static Music instance;
        public AudioSource audioSource;
        public AudioClip myCurrentSong;
        public AudioMixer masterMixer;

        private void Start() {
            instance = this;
            masterMixer.SetFloat("music", Mathf.Log(GameMaster.GetSO<GameDataSO>().musicMaxVolume) * 20);
        }


        /// <summary>
        /// Change music volume. Accepts a float parameter between 0.001 and 1.
        /// Automatically converts float value into appropraite value for the AudioMixer.
        /// </summary>
        /// <param name="volume"></param>
        public void changeMaxVolume(float volume) {
            masterMixer.SetFloat("music", Mathf.Log(GameMaster.GetSO<GameDataSO>().musicMaxVolume) * 20);
        }

        /// <summary>
        /// Plays music on the music channel.
        /// </summary>
        /// <param name="clip"></param>
        public void playMusic(AudioClip clip) {
            if (clip == myCurrentSong) { return; }
            myCurrentSong = clip;
            audioSource.clip = clip;
            audioSource.Play();
        }

        /// <summary>
        /// Stops playing music.
        /// </summary>
        public void stopMusic() {
            audioSource.Stop();
        }

        /// <summary>
        /// Fade out music.
        /// </summary>
        /// <param name="fadetime"></param>
        public void fadeOutMusic(float fadetime) {
            StartCoroutine(DoFadeOut(fadetime));
        }

        private IEnumerator DoFadeOut(float fadetime) {
            float tempmusicMaxVolume = GameMaster.GetSO<GameDataSO>().musicMaxVolume;

            while (tempmusicMaxVolume > fadetime)
            {
                tempmusicMaxVolume -= fadetime;
                if (tempmusicMaxVolume <= 0.001f) { break; }

                masterMixer.SetFloat("music", Mathf.Log(tempmusicMaxVolume) * 20);
                yield return new WaitForSeconds(fadetime);
            }

            audioSource.Stop();
            masterMixer.SetFloat("music", Mathf.Log(GameMaster.GetSO<GameDataSO>().musicMaxVolume) * 20);
        }

        /// <summary>
        /// Reduce music volume temporarily.
        /// </summary>
        public void DuckMusic() {
            float duckamount = GameMaster.GetSO<GameDataSO>().musicMaxVolume - 0.1f;

            masterMixer.SetFloat("music", Mathf.Log(duckamount) * 20);
        }

        /// <summary>
        /// Restore music volume after ducking.
        /// </summary>
        public void UnDuckMusic() {
            masterMixer.SetFloat("music", Mathf.Log(GameMaster.GetSO<GameDataSO>().musicMaxVolume) * 20);
        }

    }
}