using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase{
    public class DontDestroyLoad : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}