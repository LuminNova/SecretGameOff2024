using UnityEngine;
using TMPro;
using System.Collections;
public class SecretManager : MonoBehaviour
{
    public static SecretManager instance;
    [SerializeField] private TMP_Text secretDisplay;

    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }
    private int secretCount = 0;

    private void OnGUI() {
        secretDisplay.text = secretCount.ToString();
    }
    public void SecretCount(int num) {
        secretCount += num;
    }
}
