using UnityEngine;

/// <summary>
/// GameDataSO: Save, Load, Delete one file for Game System Data.
/// This is for saving information like audio volumes, keyboard, gamepad or input customizations.
/// Typically for system information that is not changed frequently by the player or game logic.
/// </summary>

namespace GameBase {
    //[CreateAssetMenu]
    public class GameDataSO : ScriptableObject {

        [Header("Audio Volumes")]
        public float masterMaxVolume = 1;
        public float musicMaxVolume = 1;
        public float soundfxMaxVolume = 1;
        public float voiceMaxVolume = 1;

    }
}