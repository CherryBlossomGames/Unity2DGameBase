using System.Text;
using UnityEngine;

/// <summary>
/// Simple XOR "Encryption". This is not real encryption and does not provide any level of protection for your data files
/// again unwanted decompilation. It will not deter even a beginner computer programmer from decrypting your data file.
/// If your save files must be encrypted please use something different.
/// </summary>
namespace GameBase {
    public class SimpleEncryptDecrypt : MonoBehaviour {
        private static int szEncryptionKey = 152;

        public static string EncryptDecrypt(string szPlainText) {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++) {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }
    }
}