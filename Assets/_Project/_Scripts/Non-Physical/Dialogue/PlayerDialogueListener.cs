using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MagnetFishing
{
    public class PlayerDialogueListener : MonoBehaviour
    {
        [SerializeField] private FishingRod _fishingRod;
        [SerializeField] private XRRayInteractor _leftRayInteractor;
        [SerializeField] private XRRayInteractor _rightRayInteractor;

        private void Awake()
        {
            GameSignals.MAIN_DIALOGUE_STARTED.AddListener(DisableFishingRod);
            GameSignals.MAIN_DIALOGUE_STARTED.AddListener(SetRayDistanceToHundred);
            GameSignals.MAIN_DIALOGUE_FINISHED.AddListener(EnableFishingRod);
            GameSignals.MAIN_DIALOGUE_FINISHED.AddListener(SetRayDistanceToZero);
        }

        private void OnDestroy()
        {
            GameSignals.MAIN_DIALOGUE_STARTED.RemoveListener(DisableFishingRod);
            GameSignals.MAIN_DIALOGUE_STARTED.RemoveListener(SetRayDistanceToHundred);
            GameSignals.MAIN_DIALOGUE_FINISHED.RemoveListener(EnableFishingRod);
            GameSignals.MAIN_DIALOGUE_FINISHED.AddListener(SetRayDistanceToZero);
        }

        private void SetRayDistanceToHundred(ISignalParameters parameters)
        {
            _leftRayInteractor.maxRaycastDistance = 100;
            _rightRayInteractor.maxRaycastDistance = 100;
        }

        private void SetRayDistanceToZero(ISignalParameters parameters)
        {
            _leftRayInteractor.maxRaycastDistance = 0;
            _rightRayInteractor.maxRaycastDistance = 0;
        }

        private void DisableFishingRod(ISignalParameters parameters)
        {
            _fishingRod.gameObject.SetActive(false);
        }

        private void EnableFishingRod(ISignalParameters parameters)
        {
            _fishingRod.gameObject.SetActive(true);
        }
    }
}
