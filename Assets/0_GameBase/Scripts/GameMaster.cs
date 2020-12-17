using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GameBase {
    public static class GameMaster {

        #region Premade Load, Delete and Save Functions
        public static int CurrentSlot = 1;

        /// <summary>
        /// Loads Game System Data.
        /// </summary>
        public static void LoadGameData() => LoadData("GameData", GetSO<GameDataSO>());
        /// <summary>
        /// Saves Game System Data.
        /// </summary>
        public static void SaveGameData() => SaveData("GameData", GetSO<GameDataSO>());
        /// <summary>
        /// Deletes Game System Data.
        /// </summary>
        public static void DeleteGameData() => DeleteData("GameData");

        /// <summary>
        /// Loads Player Data. Optional slotNum gives the ability for the player to have multiple different save files.
        /// </summary>
        /// <param name="slotNum"></param>
        public static void LoadPlayerSO(int slotNum) => LoadData("PlayerData", GetSO<PlayerSO>(), slotNum);
        /// <summary>
        /// Saves Player Data. Optional slotNum gives the ability for the player to have multiple different save files.
        /// </summary>
        /// <param name="slotNum"></param>
        public static void SavePlayerSO(int slotNum) => SaveData("PlayerData", GetSO<PlayerSO>(), slotNum);
        /// <summary>
        /// Deletes Player Data. Optional slotNum gives the ability for the player to have multiple different save files.
        /// </summary>
        /// <param name="slotNum"></param>
        public static void DeletePlayerSO(int slotNum) => DeleteData("PlayerData", slotNum);

        #endregion

        //Load only once
        private static bool inited = false;

        //Automatic Loading of prefabs and SO dictionary.
        private static Dictionary<Type, ScriptableObject> soInstances = new Dictionary<Type, ScriptableObject>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SpawnAllSingletons() {
            if (inited) { return; }

            var prefabs = Resources.LoadAll<GameObject>("AutoLoad");
            foreach (var p in prefabs) {
                var instance = GameObject.Instantiate(p);
                GameObject.DontDestroyOnLoad(instance);
            }

            var scriptableObjects = Resources.LoadAll<ScriptableObject>("AutoLoad");
            foreach (var so in scriptableObjects) {
                var type = so.GetType();
                var runtimeCopy = ScriptableObject.Instantiate(so);
                soInstances.Add(type, runtimeCopy);
            }

            //Done
            inited = true;
        }

        /// <summary>
        /// Usage: GameMaster.GetSO<GameDataSO>().variablename
        /// Or: GameMaster.GetSO<PlayerSO>().variablename
        /// </summary>
        /// <typeparam name="T">ScriptableObjectName</typeparam>
        /// <returns></returns>
        public static T GetSO<T>() where T : ScriptableObject {
            var type = typeof(T);
            return (T)soInstances[type];
        }

        #region Save, Load Delete
        private static void LoadData(string filename, ScriptableObject mySO, int slot = 1) {
            string mySlotString = Application.persistentDataPath + "/" + filename + slot + ".dat";
            if (File.Exists(mySlotString)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(mySlotString, FileMode.Open);
                JsonUtility.FromJsonOverwrite(SimpleEncryptDecrypt.EncryptDecrypt((string)bf.Deserialize(file)), mySO);
                file.Close();
            }
        }

        private static void SaveData(string filename, ScriptableObject mySO, int slot = 1) {
            string mySlotString = Application.persistentDataPath + "/" + filename + slot + ".dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(mySlotString);
            var json = SimpleEncryptDecrypt.EncryptDecrypt(JsonUtility.ToJson(mySO));
            bf.Serialize(file, json);
            file.Close();
        }

        private static void DeleteData(string filename, int slot = 1) {
            string mySlotString = Application.persistentDataPath + "/" + filename + slot + ".dat";
            try {
                Debug.Log("Deleted: " + mySlotString);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                Debug.Log("Can't delete file: " + mySlotString);
            }
        }
        #endregion
    }
}