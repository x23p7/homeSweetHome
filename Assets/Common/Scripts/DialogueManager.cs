using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;
    private Queue<string> sentences;
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
        sentences = new Queue<string>();
	}

    public void StartDialogue(DialogueTrigger dialogueTrigger,Dialogue dialogue)
    {
        panelAnim.SetBool("panelActive", true);
        nameAnim.SetBool("textActive", true);
        dialogueAnim.SetBool("dialogueActive", true);
        currentDialogueTrigger = dialogueTrigger;
        currentDialogueTrigger.dialogueActive = true;
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void EndDialogue()
    {
        currentDialogueTrigger.dialogueActive = false;
        panelAnim.SetBool("panelActive", false);
        nameAnim.SetBool("textActive", false);
        dialogueAnim.SetBool("dialogueActive", false);
        Debug.Log("Ending Conv");
    }
}
