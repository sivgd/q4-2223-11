using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialDialogue : MonoBehaviour
{
   // public Animator blackScreen;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public Sprite characterSprite;
    public GameObject characterIcon;
    public Sprite[] sprites;
    public string[] names;
    public string[] lines;
    public float textSpeed;
    public int index;
    public GameObject player;
    public Animator door1;
    public Animator door2;

    public tpMovement moveScript;
    

    public pauseButtons PB;
    // Start is called before the first frame update
    void Start()
    {
       // blackScreen.Play("BlackScreenTransition");
        index = 1;
        //blackScreen.SetInteger("IndexNum", index);
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        characterSprite = sprites[0];
        nameComponent.text = names[0];
        characterIcon.GetComponent<Image>().sprite = characterSprite;
        //GetComponent<tpMovement>().canMove = false;
        moveScript = player.GetComponent<tpMovement>();
        moveScript.canMove = false;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PB.isPaused)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
                characterSprite = sprites[index];
                nameComponent.text = names[index];
                characterIcon.GetComponent<Image>().sprite = sprites[index];
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                nameComponent.text = names[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            moveScript.canMove = true;
            door1.SetTrigger("Open1");
            door2.SetTrigger("Open2");
            // blackScreen.Play("BlackScreenTransition(Negitive)");
        }
    }
}
