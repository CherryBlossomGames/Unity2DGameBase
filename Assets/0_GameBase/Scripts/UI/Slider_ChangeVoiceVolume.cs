using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameBase {
    public class Slider_ChangeVoiceVolume : MonoBehaviour {
        public Slider slider;
        public TextMeshProUGUI tmp;

        private void Start() {
            slider.value = GameMaster.GetSO<GameDataSO>().voiceMaxVolume;

            tmp.text = "Voice " + (int)(slider.value * 100) + "%";
        }

        public void OnChangedSlider() {
            GameMaster.GetSO<GameDataSO>().voiceMaxVolume = slider.value;
            Voice.instance.changeMaxVolume(slider.value);

            tmp.text = "Voice " + (int)(slider.value * 100) + "%";
        }
    }
}