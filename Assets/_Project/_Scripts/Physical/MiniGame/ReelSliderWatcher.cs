using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagnetFishing
{
    public class ReelSliderWatcher : MonoBehaviour
    {
        [SerializeField] private Image _reelSlider;
        [Range(0, 0.03f)]
        [SerializeField] private float _reelStrength;

        private GameObject _gameHolder;
        private GameObject _textCanvas;
        private bool _isReeling;
        private bool _miniGameWon;
        private bool _wonEventsDispatched;
        private float _reelPercentage;

        private void Awake()
        {
            _gameHolder = transform.GetChild(0).gameObject;
            _textCanvas = transform.GetChild(1).gameObject;

            GameSignals.REELING_IN.AddListener(ReelingIn);
            GameSignals.NOT_REELING_IN.AddListener(NotReelingIn);
        }

        private void OnDestroy()
        {
            GameSignals.REELING_IN.RemoveListener(ReelingIn);
            GameSignals.NOT_REELING_IN.RemoveListener(NotReelingIn);

            if (_miniGameWon && !_wonEventsDispatched)
            {
                GameSignals.FISH_CAUGHT.Dispatch();
                GameSignals.START_NEXT_MAIN_DIALOGUE.Dispatch();
            }
        }

        private void Start()
        {
            _textCanvas.SetActive(false);
        }

        private void FixedUpdate()
        {
            ReelHandle();
        }

        private void ReelHandle()
        {
            if (_miniGameWon) return;

            if (_isReeling)
            {
                _reelPercentage += _reelStrength;

                if (_reelPercentage >= 1)
                {
                    StartCoroutine(Caught());
                }
            }
            else
            {
                _reelPercentage -= _reelStrength;

                if (_reelPercentage <= 0)
                    _reelPercentage = 0;
            }

            UpdateReelSlider(_reelPercentage);
        }

        private IEnumerator Caught()
        {
            _gameHolder.SetActive(false);
            _textCanvas.SetActive(true);
            _miniGameWon = true;

            yield return new WaitForSeconds(3f);

            GameSignals.FISH_CAUGHT.Dispatch();
            GameSignals.START_NEXT_MAIN_DIALOGUE.Dispatch();

            _wonEventsDispatched = true;
            _textCanvas.SetActive(false);
        }

        private void ReelingIn(ISignalParameters parameters)
        {
            _isReeling = true;
        }

        private void NotReelingIn(ISignalParameters parameters)
        {
            _isReeling = false;
        }

        private void UpdateReelSlider(float percentage)
        {
            _reelSlider.fillAmount = percentage;
        }
    }
}
