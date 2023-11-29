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

        private bool _isReeling;
        private bool _miniGameWon;
        private float _reelPercentage;

        private void Awake()
        {
            GameSignals.REELING_IN.AddListener(ReelingIn);
            GameSignals.NOT_REELING_IN.AddListener(NotReelingIn);
        }

        private void OnDestroy()
        {
            GameSignals.REELING_IN.RemoveListener(ReelingIn);
            GameSignals.NOT_REELING_IN.RemoveListener(NotReelingIn);
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
                    GameSignals.FISH_CAUGHT.Dispatch();
                    GameSignals.START_NEXT_MAIN_DIALOGUE.Dispatch();
                    _miniGameWon = true;
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
