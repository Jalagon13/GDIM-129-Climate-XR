using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MagnetFishing
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private Hook _hookObject;
        [SerializeField] private Transform _rodTipTransform;

        private Vector3 _startingPos;
        private Quaternion _startingRot;
        private static Hook _hook;

        private void Awake()
        {
            _startingPos = transform.position;
            _startingRot = transform.rotation;

            GameSignals.POWER_RELEASED.AddListener(ThrowHook);
        }

        private void OnDestroy()
        {
            GameSignals.POWER_RELEASED.RemoveListener(ThrowHook);
        }

        private void ThrowHook(ISignalParameters parameters)
        {
            DestroyHook();

            _hook = Instantiate(_hookObject, _rodTipTransform.position += new Vector3(0, 0.15f, 0), Quaternion.identity);
            _hook.InitializeHook(_rodTipTransform);
        }

        private void DestroyHook()
        {
            if (_hook != null)
                Destroy(_hook.gameObject);
        }

        // called when front trigger is pressed
        public void Activate(ActivateEventArgs args)
        {
            GameSignals.ROD_ACTIVATED.Dispatch();
        }

        // called when front trigger is released
        public void DeActivate(DeactivateEventArgs args)
        {
            GameSignals.ROD_DEACTIVATED.Dispatch();
        }

        // called when rod is selected
        public void FirstSelectEnter(SelectEnterEventArgs args)
        {
            Debug.Log("Selecting Rod with");
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
            DestroyHook();
            transform.SetPositionAndRotation(_startingPos, _startingRot);
        }
    }
}
