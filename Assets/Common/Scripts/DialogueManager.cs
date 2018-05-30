using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private Queue<Sentence> sentences;
    DialogueTrigger currentDialogueTrigger;
    public float letterDelay;
    public Text nameText;
    public Text dialogueText;

    public Animator panelAnim;
    public Animator nameAnim;
    public Animator dialogueAnim;

    public Text[] choiceTexts;
    public Animator[] choiceFrameAnimators;
    public Animator[] choiceTextAnimators;
    public Animator[] choiceSpriteAnimators;
    public Image[] choiceSprites;
    public Color[] xBoxColors;
    public Color[] PS4Colors;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<Sentence>();
    }

    public void StartDialogue(DialogueTrigger dialogueTrigger, Dialogue dialogue)
    {
        panelAnim.SetBool("panelActive", true);
        nameAnim.SetBool("panelActive", true);
        dialogueAnim.SetBool("dialogueActive", true);
        currentDialogueTrigger = dialogueTrigger;
        currentDialogueTrigger.dialogueActive = true;
        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (currentDialogueTrigger.choicesAfterDialogue)
            {
                StartChoice(currentDialogueTrigger, currentDialogueTrigger.choices);
            }
            else
            {
                currentDialogueTrigger.TriggerOutcome(currentDialogueTrigger.myEffects);
            }
            EndDialogue();
            return;
        }
        Sentence sentence = sentences.Dequeue();
        nameText.text = sentence.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.text, dialogueText));
    }

    IEnumerator TypeSentence(string sentenceText, Text targetText)
    {
        targetText.text = "";
        foreach (char letter in sentenceText.ToCharArray())
        {
            targetText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void EndDialogue()
    {
        InputManager.instance.actionInputDown = false;
        currentDialogueTrigger.dialogueActive = false;
        panelAnim.SetBool("panelActive", false);
        nameAnim.SetBool("panelActive", false);
        dialogueAnim.SetBool("dialogueActive", false);
    }

    public void StartChoice(DialogueTrigger dialogueTrigger, Choice[] choices)
    {
        currentDialogueTrigger = dialogueTrigger;
        currentDialogueTrigger.choicesActive = true;
        if (choices.Length > choiceTexts.Length)
        {
            Debug.Log("too many choices");
            return;
        }
        for (int i = choices.Length; i > 0; i--)
        {
            if (choices[i - 1].choiceLabel != "")
            {
                if (InputManager.instance.activeInputScript == InputManager.instance.XboxControles)
                {
                    choiceSprites[i - 1].color = xBoxColors[i - 1];
                }
                else
                {
                    choiceSprites[i - 1].color = PS4Colors[i - 1];
                }
                choiceFrameAnimators[i - 1].SetBool("panelActive", true);
                choiceTextAnimators[(i - 1)].SetBool("panelActive", true);
                choiceSpriteAnimators[(i - 1)].SetBool("panelActive", true);


                StartCoroutine(TypeSentence(choices[i - 1].choiceLabel, choiceTexts[i - 1]));
            }
            StartCoroutine(MakeAChoice(currentDialogueTrigger));
        }
    }

    IEnumerator MakeAChoice(DialogueTrigger currentDialogueTrigger)
    {
        InputManager.instance.choiceOne = false;
        InputManager.instance.choiceTwo = false;
        InputManager.instance.choiceThree = false;
        InputManager.instance.choiceFour = false;
        InputManager.instance.disabled = true;
        while (!(InputManager.instance.choiceOne && currentDialogueTrigger.choices[0].choiceLabel != "" ||
            InputManager.instance.choiceTwo && currentDialogueTrigger.choices[1].choiceLabel != "" ||
            InputManager.instance.choiceThree && currentDialogueTrigger.choices[2].choiceLabel != "" ||
            InputManager.instance.choiceFour && currentDialogueTrigger.choices[3].choiceLabel != ""))
        {
            yield return null;
        }
        if (InputManager.instance.choiceOne)
        {
            currentDialogueTrigger.TriggerOutcome(currentDialogueTrigger.choices[0].choiceEffects);
            if (currentDialogueTrigger.choices[0].continuesDialogue)
            {
                InputManager.instance.actionInputDown = true;
            }
        }
        if (InputManager.instance.choiceTwo)
        {
            currentDialogueTrigger.TriggerOutcome(currentDialogueTrigger.choices[1].choiceEffects);
            if (currentDialogueTrigger.choices[1].continuesDialogue)
            {
                InputManager.instance.actionInputDown = true;
            }
        }
        if (InputManager.instance.choiceThree)
        {
            currentDialogueTrigger.TriggerOutcome(currentDialogueTrigger.choices[2].choiceEffects);
            if (currentDialogueTrigger.choices[3].continuesDialogue)
            {
                InputManager.instance.actionInputDown = true;
            }
        }
        if (InputManager.instance.choiceFour)
        {
            currentDialogueTrigger.TriggerOutcome(currentDialogueTrigger.choices[3].choiceEffects);
            if (currentDialogueTrigger.choices[3].continuesDialogue)
            {
                InputManager.instance.actionInputDown = true;
            }
        }
        foreach (Animator animator in choiceFrameAnimators)
        {
            animator.SetBool("panelActive", false);
        }
        foreach (Animator animator in choiceTextAnimators)
        {
            animator.SetBool("panelActive", false);
        }
        foreach (Animator animator in choiceSpriteAnimators)
        {
            animator.SetBool("panelActive", false);
        }
        currentDialogueTrigger.TriggerOutcome(currentDialogueTrigger.myEffects);
        InputManager.instance.disabled = false;
    }
}
