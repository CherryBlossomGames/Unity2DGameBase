using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameBase {
    public class ResolutionSetBoot : MonoBehaviour {
        private const string RESOLUTION_PREF_KEY = "resolution";
        private Resolution[] resolutions;
        private int currentResolutionIndex = 0;

        private const string FULLSCREEN_PREF_KEY = "fullscreen";
        private int currentFS = 0;

        private void Start() {
            currentFS = PlayerPrefs.GetInt(FULLSCREEN_PREF_KEY, 1);
            resolutions = Screen.resolutions;
            resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
            currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, -1);

            //If the player preference is set, then just set the text and resolution.
            if (currentResolutionIndex != -1) {
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
                    }
                    count++;
                }
            }


        }

        private void SetAndApplyResolution(int newResolutionIndex) {
            currentResolutionIndex = newResolutionIndex;
            ApplyCurrentResolution();
        }

        private void ApplyCurrentResolution() {
            ApplyResolution(resolutions[currentResolutionIndex]);
        }

        private void ApplyResolution(Resolution resolution) {
            PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
            PlayerPrefs.SetInt(FULLSCREEN_PREF_KEY, currentFS);
            if (currentFS == 0) { Screen.SetResolution(resolution.width, resolution.height, false); }
            if (currentFS == 1) { Screen.SetResolution(resolution.width, resolution.height, true); }
        }

        public void SetNextResolution()
        {
            currentResolutionIndex = GetNextWrappedIndex(resolutions, currentResolutionIndex);
        }

        public void SetPreviousResolution()
        {
            currentResolutionIndex = GetPreviousWrappedIndex(resolutions, currentResolutionIndex);
        }

        private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
        {
            if (collection.Count < 1) { return 0; }
            return (currentIndex + 1) % collection.Count;
        }

        private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
        {
            if (collection.Count < 1) { return 0; }
            if ((currentIndex - 1) < 0) { return collection.Count - 1; }
            return (currentIndex - 1) % collection.Count;
        }

        //Apply Changes Button
        public void ApplyChanges()
        {
            SetAndApplyResolution(currentResolutionIndex);
        }

    }
}