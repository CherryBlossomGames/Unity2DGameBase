using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameBase
{
    public class SliderChangeMusicVolume : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI tmp;

        private void Start()
        {
            slider.value = GameMaster.GetSO<GameDataSO>().musicMaxVolume;

            tmp.text = "Music " + (int)(slider.value * 100) + "%";
        }


        public void OnChangedSlider()
        {
            GameMaster.GetSO<GameDataSO>().musicMaxVolume = slider.value;
            Music.instance.changeMaxVolume(slider.value);

            tmp.text = "Music " + (int)(slider.value * 100) + "%";
        }

    }
}