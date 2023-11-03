using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MagnetFishing
{
    public class FishingRod : MonoBehaviour
    {
        private Vector3 _startingPos;
        private Quaternion _startingRot;

        private void Awake()
        {
            _startingPos = transform.position;
            _startingRot = transform.rotation;
        }

        // called when front trigger is pressed
        public void Activate(ActivateEventArgs args)
        {
            Debug.Log("Activated");
        }

        // called when front trigger is released
        public void DeActivate(DeactivateEventArgs args)
        {
            Debug.Log("De-Activate");
        }

        // called when rod is selected
        public void FirstSelectEnter()
        {
            Debug.Log("Selecting Rod");
        }

        // caled when rod is deselected
        public void LastSelectExit()
        {
            Debug.Log("De Selecting Rod");
            StartCoroutine(ReturnToStartingPos());
        }

        private IEnumerator ReturnToStartingPos()
        {
            yield return new WaitForSeconds(0.75f);
            transform.SetPositionAndRotation(_startingPos, _startingRot);
        }
    }
}
