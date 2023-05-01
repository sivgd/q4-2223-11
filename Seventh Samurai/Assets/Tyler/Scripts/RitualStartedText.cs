using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RitualStartedText : MonoBehaviour
{
    public TMP_Text textLabel;
    public string Text;
    public int textSpeed;

    public void Run(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * textSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = textToType;

    }

    private void Start()
    {
        GetComponent<RitualStartedText>().Run(Text, textLabel);
    }


}
