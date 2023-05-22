using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    private bool isDialogActive = false;
    private SphereCollider speakCollider;

    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private GameObject dialogCanvas;

    void OnEnable()
    {
        dialogTexts = new string[dialog.Count];

        speakCollider = GetComponent<SphereCollider>();
        speakCollider.radius = speakRange;
        speakCollider.transform.position = transform.position;
        speakCollider.isTrigger = true;

        dialogCanvas.SetActive(false);
    }
    public void StartDialog()
    {
        if (isSpeaking || currentSentenceIndex > dialog[currentDialogIndex].sentences.Length - 1) return;
        dialogCanvas.SetActive(true);
        StartCoroutine(TypeText(dialog[currentDialogIndex]
        .sentences[currentSentenceIndex]
        .localizedSentence.GetLocalizedString()));
        isDialogActive = true;
        isSpeaking = true;
    }

    IEnumerator TypeText(string text)
    {
        currentText = "";
        characterName.text = dialog[currentDialogIndex].sentences[currentSentenceIndex].name;
        foreach (char letter in text.ToCharArray())
        {
            currentText += letter;
            dialogText.text = currentText;
            yield return new WaitForSeconds(dialog[currentDialogIndex].sentences[currentSentenceIndex].delay);
        }
        currentSentenceIndex++;
        dialog[currentDialogIndex].sentences[currentSentenceIndex].OnSentenceComplete?.Invoke();
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
            StartDialog();
        }
    }

    private void Update()
    {
        if (isDialogActive)
        {
            if (Input.anyKeyDown)
            {
                if (isSpeaking)
                {
                    StopAllCoroutines();
                    dialogText.text = dialog[currentDialogIndex].sentences[currentSentenceIndex].localizedSentence.GetLocalizedString();
                    isSpeaking = false;
                    currentSentenceIndex++;
                    dialog[currentDialogIndex].sentences[currentSentenceIndex].OnSentenceComplete?.Invoke();
                }
                else if (currentSentenceIndex > dialog[currentDialogIndex].sentences.Length - 1)
                {
                    Debug.Log("End of dialog");
                    dialogCanvas.SetActive(false);
                    currentSentenceIndex = 0;
                    dialog[currentDialogIndex].OnDialogComplete?.Invoke();
                    isDialogActive = false;
                }
                else
                {
                    StartDialog();
                }
            }
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
    public void SetDialog(string identifier)
    {
        if (dialog.Count == 0) return;
        if (dialog.Contains(dialog.Find(x => x.identifier == identifier)))
        {
            currentDialogIndex = dialog.IndexOf(dialog.Find(x => x.identifier == identifier));
        }
    }
}
