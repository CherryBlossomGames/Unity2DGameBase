using UnityEngine;
using UnityEngine.Audio;

namespace GameBase
{
    public class MasterAudio : MonoBehaviour
    {

        public static MasterAudio instance;

        public AudioMixer masterMixer;

        private void Start()
        {
            instance = this;

            masterMixer.SetFloat("master", Mathf.Log(GameMaster.GetSO<GameDataSO>().masterMaxVolume) * 20);
        }

        /// <summary>
        /// Changes Volume for the Master Audio channel. This method takes a float value between 0.01 and 1
        /// and converts it to a proper value for the audio mixer using Mathf.Log(volume) * 20
        /// </summary>
        /// <param name="volume"></param>
        public void changeMaxVolume(float volume)
        {
            masterMixer.SetFloat("master", Mathf.Log(volume) * 20);
        }

    }
}