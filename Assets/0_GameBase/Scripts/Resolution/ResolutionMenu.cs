using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace ScalesRPG
{
    public class ResolutionMenu : MonoBehaviour
    {

        //Components
        [Header("Text Mesh Pro - Resolution Text")]
        [SerializeField] private TextMeshProUGUI resolutionTPMtext = default;
        [Header("Text Mesh Pro - FullScreen Text")]
        [SerializeField] private TextMeshProUGUI fullscreenTPMtext = default;

        //Strings for saving to player prefs
        private const string FULLSCREEN_PREF_KEY = "fullscreen";
        private const string RESOLUTION_PREF_KEY = "resolution";

        //Computed vars
        private Resolution[] resolutions;
        private int currentResolutionIndex = 0;
        public int currentFS = 0;


        private void Awake() {
            SetupResolution();
            fullScreenText();
        }

        private void SetupResolution() {
            currentFS = PlayerPrefs.GetInt(FULLSCREEN_PREF_KEY, 1);
            resolutions = Screen.resolutions;
            resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
            currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, -1);

            //If the player preference is set, then just set the text and resolution.
            if (currentResolutionIndex != -1) {
                SetResolutionText(resolutions[currentResolutionIndex]);
                SetAndApplyResolution(currentResolutionIndex);
            }

            //If no playerpref for resolution is saved, try to find a match for display
            if (currentResolutionIndex == -1) {
                int count = 0;
                string current1 = Screen.currentResolution.ToString();
                string result1 = current1.Substring(0, current1.LastIndexOf("@"));
                foreach (Resolution element in resolutions) {
                    string element1 = element.ToString();
                    string result2 = element1.Substring(0, element1.LastIndexOf("@"));

                    // Match found, set resolution text
                    if (result1 == result2) {
                        currentResolutionIndex = count;
                        SetResolutionText(resolutions[currentResolutionIndex]);
                    }
                    count++;
                }
            }
        }

        public void ResetResolutionTextOutput() {
            SetupResolution();
        }

        private void SetAndApplyResolution(int newResolutionIndex) {
            currentResolutionIndex = newResolutionIndex;
            ApplyCurrentResolution();
        }

        private void ApplyCurrentResolution() {
            ApplyResolution(resolutions[currentResolutionIndex]);
        }

        private void ApplyResolution(Resolution resolution) {
            SetResolutionText(resolution);
            PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
            PlayerPrefs.SetInt(FULLSCREEN_PREF_KEY, currentFS);
            if (currentFS == 0) { Screen.SetResolution(resolution.width, resolution.height, false); }
            if (currentFS == 1) { Screen.SetResolution(resolution.width, resolution.height, true); }
        }

        private void SetResolutionText(Resolution resolution) {
            resolutionTPMtext.text = resolution.width + " x " + resolution.height;
        }

        //Update resolution text description when object is enabled.
        private void OnEnable() {
            if (resolutionTPMtext != null)
                resolutionTPMtext.text = resolutions[currentResolutionIndex].width + " x " + resolutions[currentResolutionIndex].height;
        }

        public void SetNextResolution() {
            currentResolutionIndex = GetNextWrappedIndex(resolutions, currentResolutionIndex);
            SetResolutionText(resolutions[currentResolutionIndex]);
        }

        public void SetPreviousResolution() {
            currentResolutionIndex = GetPreviousWrappedIndex(resolutions, currentResolutionIndex);
            SetResolutionText(resolutions[currentResolutionIndex]);
        }

        private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex) {
            if (collection.Count < 1) { return 0; }
            return (currentIndex + 1) % collection.Count;
        }

        private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex) {
            if (collection.Count < 1) { return 0; }
            if ((currentIndex - 1) < 0) { return collection.Count - 1; }
            return (currentIndex - 1) % collection.Count;
        }

        //Apply Changes Button
        public void ApplyChanges() {
            SetAndApplyResolution(currentResolutionIndex);
        }

        //Toggle Fullscreen using Button and show on Text
        public void OnButtonToggleFS() {
            if (currentFS == 0) {
                currentFS = 1;
            } else {
                currentFS = 0;
            }
            fullScreenText();
        }


        private void fullScreenText() {
            if (currentFS == 0) {
                fullscreenTPMtext.text = "FullScreen";
            }
            else {
                fullscreenTPMtext.text = "Windowed";
            }
        }

    }
}