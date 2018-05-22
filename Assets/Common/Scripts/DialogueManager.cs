using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;
    private Queue<Sentence> sentences;
    DialogueTrigger currentDialogueTrigger;
    public float letterDelay;
    public Text nameText;
    public Text dialogueText;

    public Animator panelAnim;
    public Animator nameAnim;
    public Animator dialogueAnim;
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
    void Start () {
        sentences = new Queue<Sentence>();
	}

    public void StartDialogue(DialogueTrigger dialogueTrigger,Dialogue dialogue)
    {
        panelAnim.SetBool("panelActive", true);
        nameAnim.SetBool("textActive", true);
        dialogueAnim.SetBool("dialogueActive", true);
        currentDialogueTrigger = dialogueTrigger;
        currentDialogueTrigger.dialogueActive = true;
        sentences.Clear();
        foreach(Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            currentDialogueTrigger.TriggerOutcome();
            EndDialogue();
            return;
        }
        Sentence sentence = sentences.Dequeue();
        nameText.text = sentence.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (Sentence sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void EndDialogue()
    {
        InputManager.instance.actionInputDown = false;
        currentDialogueTrigger.dialogueActive = false;
        panelAnim.SetBool("panelActive", false);
        nameAnim.SetBool("textActive", false);
        dialogueAnim.SetBool("dialogueActive", false);
    }
}
