using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

[RequireComponent(typeof(SphereCollider))]
public class Speaker : MonoBehaviour
{
    public float speakRange = 5f;
    public List<Dialog> dialog;
    private Sentence currentSentence;
    public static string currentText = "";
    public int currentDialogIndex = 0;
    private string[] dialogTexts;
    private int currentSentenceIndex = 0;
    private bool isSpeaking = false;
    private SphereCollider speakCollider;

    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI characterName;

    void OnEnable()
    {
        dialogTexts = new string[dialog.Count];
        for (int i = 0; i < dialog.Count; i++)
        {
            foreach (Sentence currentDialogSentence in dialog[i].sentences)
            {
                Debug.Log(currentDialogSentence.localizedSentence.GetLocalizedString());
            }
            // dialogTexts[i] = dialog[i].sentences[i].sentence;
            // dialogTexts[i] = dialog[i].sentences[i].localizedSentence.GetLocalizedString();
        }

        speakCollider = GetComponent<SphereCollider>();
        speakCollider.radius = speakRange;
        speakCollider.transform.position = transform.position;
        speakCollider.isTrigger = true;
    }
    public void StartDialog()
    {
        if (isSpeaking || currentSentenceIndex > dialog[currentDialogIndex].sentences.Length - 1) return;
        StartCoroutine(TypeText(dialog[currentDialogIndex]
        .sentences[currentSentenceIndex]
        .localizedSentence.GetLocalizedString()));
        isSpeaking = true;
    }

    IEnumerator TypeText(string text)
    {
        currentText = "";
        if(currentSentenceIndex > dialog[currentDialogIndex].sentences.Length - 1){
            currentSentenceIndex = 0;
            isSpeaking = false;
            yield break;
        }
        characterName.text = dialog[currentDialogIndex].sentences[currentSentenceIndex].name;
        foreach (char letter in text.ToCharArray())
        {
            currentText += letter;
            dialogText.text = currentText;
            yield return new WaitForSeconds(0.03f);
        }
        currentSentenceIndex++;
        isSpeaking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, speakRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // DialogManager.Dialoguemanager.StartDialogue(dialog[0]);
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 100, 100, 30), "Test Dialogue"))
        {
            StartDialog();
        }
    }

    public void SetDialogIndex(int index)
    {
        currentDialogIndex = index;
    }
    public void SetDialog(string identifier){
        if(dialog.Count == 0) return;
        if(dialog.Contains(dialog.Find(x => x.identifier == identifier))){
            currentDialogIndex = dialog.IndexOf(dialog.Find(x => x.identifier == identifier));
        }
    }
}
