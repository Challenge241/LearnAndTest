using UnityEngine.UI;
using UnityEngine;

public class TryMessageDisplay : MonoBehaviour
{
    public Text welcomeText;
    public string key;
    private void OnEnable()
    {
        LanguageManager.OnLanguageChanged += UpdateText;
    }
    private void OnDestroy()
    {
        LanguageManager.OnLanguageChanged -= UpdateText;
    }
    void UpdateText()
    {
        welcomeText.text = LanguageManager.Instance.GetLocalizedText(key);;
    }
}
