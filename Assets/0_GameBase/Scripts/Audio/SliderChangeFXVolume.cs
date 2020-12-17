using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameBase
{
    public class SliderChangeFXVolume : MonoBehaviour
    {

        public Slider slider;
        public TextMeshProUGUI tmp;

        private void Start()
        {
            slider.value = GameMaster.GetSO<GameDataSO>().soundfxMaxVolume;

            tmp.text = "FX " + (int)(slider.value * 100) + "%";
        }


        public void OnChangedSlider()
        {
            GameMaster.GetSO<GameDataSO>().soundfxMaxVolume = slider.value;
            SoundFX.instance.changeMaxVolume(slider.value);

            tmp.text = "FX " + (int)(slider.value * 100) + "%";
        }

    }
}