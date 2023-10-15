using UnityEngine.UI;
using UnityEngine;

public class WelcomeMessageDisplay : MonoBehaviour
{
    public Text welcomeText;
    private void OnEnable()
    {
        LanguageManager.OnLanguageChanged += UpdateText;
    }

    private void OnDisable()
    {
        LanguageManager.OnLanguageChanged -= UpdateText;
    }

    void UpdateText()
    {
        string welcomeMessageKey = "NiHao";
        welcomeText.text = LanguageManager.Instance.GetLocalizedText(welcomeMessageKey);
    }
}
