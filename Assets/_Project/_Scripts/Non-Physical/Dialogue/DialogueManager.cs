using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MagnetFishing
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private ItemDisplay _itemDisplay;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private AudioClip _buttonPressSound;
        [SerializeField] private ScriptObject[] _gameScriptOrder;

        private int _gameScriptIndex = 0;
        private int _currentDialogueIndex = 0;
        private ScriptObject _currentGameScript;
        private RectTransform _dialogueHolder;

        private void Awake()
        {
            _dialogueHolder = transform.GetChild(0).GetComponent<RectTransform>();

            GameSignals.START_NEXT_MAIN_DIALOGUE.AddListener(StartScript);
        }

        private void OnDestroy()
        {
            GameSignals.START_NEXT_MAIN_DIALOGUE.RemoveListener(StartScript);
        }

        private IEnumerator Start()
        {
            EnableHolder(false);

            yield return new WaitForSeconds(3f);

            GameSignals.START_NEXT_MAIN_DIALOGUE.Dispatch(); // If you want to disable the biginning dialogue, comment out this line of code.
        }

        private void StartScript(ISignalParameters parameters)
        {
            if (_gameScriptOrder.Length <= _gameScriptIndex)
            {
                Debug.Log("Reached the end of all of the game's Dialogue");
                return;
            }

            _currentGameScript = _gameScriptOrder[_gameScriptIndex];
            _currentDialogueIndex = 0;

            if (_currentGameScript.ItemToDisplay != null)
                _itemDisplay.Display(_currentGameScript.ItemToDisplay, _currentGameScript.ItemName);

            UpdateText();
            EnableHolder(true);
        }

        private void EndScript()
        {
            GameSignals.MAIN_DIALOGUE_FINISHED.Dispatch();

            _currentDialogueIndex = 0;
            _gameScriptIndex++;
            _itemDisplay.HideDisplay();

            EnableHolder(false);
        }

        public void NextButton()
        {
            _currentDialogueIndex++;
            MMSoundManagerSoundPlayEvent.Trigger(_buttonPressSound, MMSoundManager.MMSoundManagerTracks.Sfx, transform.position);
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
