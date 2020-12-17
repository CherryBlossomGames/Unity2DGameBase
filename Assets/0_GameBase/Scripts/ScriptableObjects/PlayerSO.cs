using UnityEngine;

/// <summary>
/// PlayerSO: Specifically for saving game progress. Checkpoints, scene or anything else that needs to be saved to
/// preserve state between gameplay sessions should be placed here.
/// </summary>

namespace GameBase {
    //[CreateAssetMenu]
    public class PlayerSO : ScriptableObject {

        public string savedScene = default;
        public int checkpoint = 0;

        public int currentHealth = 5;
        public int maxHealth = 10;
    }
}