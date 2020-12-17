using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameBase
{
    public class SliderChangeMasterVolume : MonoBehaviour
    {

        public Slider slider;
        public TextMeshProUGUI tmp;

        private void Start()
        {
            slider.value = GameMaster.GetSO<GameDataSO>().masterMaxVolume;

            tmp.text = "Master " + (int)(slider.value * 100) + "%";
        }


        public void OnChangedSlider()
        {
            GameMaster.GetSO<GameDataSO>().masterMaxVolume = slider.value;
            MasterAudio.instance.changeMaxVolume(slider.value);

            tmp.text = "Master " + (int)(slider.value * 100) + "%";
        }

    }
}