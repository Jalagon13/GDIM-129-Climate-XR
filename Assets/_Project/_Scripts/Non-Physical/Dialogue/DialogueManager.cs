using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MagnetFishing
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private ScriptObject[] _gameScriptOrder;

        private int _gameScriptIndex = 0;
        private int _currentDialogueIndex = 0;
        private ScriptObject _currentGameScript;
        private RectTransform _dialogueHolder;

        private void Awake()
        {
            _dialogueHolder = transform.GetChild(0).GetComponent<RectTransform>();

            GameSignals.MAIN_DIALOGUE_STARTED.AddListener(StartScript);
        }

        private void OnDestroy()
        {
            GameSignals.MAIN_DIALOGUE_STARTED.RemoveListener(StartScript);
        }

        private IEnumerator Start()
        {
            EnableHolder(false);

            yield return new WaitForSeconds(3f);

            GameSignals.MAIN_DIALOGUE_STARTED.Dispatch(); // If you want to disable the biginning dialogue, comment out this line of code.
        }

        private void StartScript(ISignalParameters parameters)
        {
            if(_gameScriptOrder.Length <= _gameScriptIndex)
            {
                Debug.Log("Reached the end of all of the game's Dialogue");
                return;
            }

            _currentGameScript = _gameScriptOrder[_gameScriptIndex];
            _currentDialogueIndex = 0;

            UpdateText();
            EnableHolder(true);
        }

        private void EndScript()
        {
            GameSignals.MAIN_DIALOGUE_FINISHED.Dispatch();

            _currentDialogueIndex = 0;
            _gameScriptIndex++;

            EnableHolder(false);
            StartCoroutine(Delay());
        }

        private IEnumerator Delay() // temp
        {
            yield return new WaitForSeconds(2f);

            if (_gameScriptOrder.Length <= _gameScriptIndex)
            {
                Debug.Log("Reached the end of all of the game's Dialogue");
            }
            else
            {
                GameSignals.MAIN_DIALOGUE_STARTED.Dispatch();
            }
        }

        public void NextButton()
        {
            _currentDialogueIndex++;

            if(_currentGameScript.DialogueScript.Length <= _currentDialogueIndex)
            {
                EndScript();
                return;
            }

            UpdateText();
        }

        private void UpdateText()
        {
            _dialogueText.text = _currentGameScript.DialogueScript[_currentDialogueIndex];
        }

        private void EnableHolder(bool _)
        {
            _dialogueHolder.gameObject.SetActive(_);
        }
    }
}
