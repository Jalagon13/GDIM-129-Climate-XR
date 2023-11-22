using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MagnetFishing
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private Hook _hook;
        [SerializeField] private Transform _rodTipTransform;

        private Vector3 _startingPos;
        private Quaternion _startingRot;

        private void Awake()
        {
            _startingPos = transform.position;
            _startingRot = transform.rotation;

            GameSignals.HOOK_RELEASED.AddListener(ThrowHook);
            GameSignals.FISH_CAUGHT.AddListener(FishCaught);
            GameSignals.FISH_GOT_AWAY.AddListener(FishGotAway);
        }

        private void Start()
        {
            DisableHook();
        }

        private void OnDestroy()
        {
            GameSignals.HOOK_RELEASED.RemoveListener(ThrowHook);
            GameSignals.FISH_CAUGHT.RemoveListener(FishCaught);
            GameSignals.FISH_GOT_AWAY.RemoveListener(FishGotAway);
        }

        private void FishCaught(ISignalParameters parameters)
        {
            DisableHook();

            // YAY FISH CAUGHT 
            // Do inventory/game feel stuff here for when fish is caught. or really anywhere as long as its listening to FISH_CAUGHT

            Debug.Log("Fish Caught!");
        }

        private void FishGotAway(ISignalParameters parameters)
        {
            DisableHook();

            // FISH GOT AWAY
            // Do inventory/game feel stuff here for when fish got away. or really anywhere as long as its listening to FISH_GOT_AWAY

            Debug.Log("Fish Got Away!");
        }

        private void ThrowHook(ISignalParameters parameters)
        {
            if (parameters.HasParameter("ForcePercentage"))
            {
                float forcePercentage = (float)parameters.GetParameter("ForcePercentage");

                DisableHook();

                if (_hook != null && _hook.gameObject != null)
                {
                    _hook.gameObject.SetActive(true);
                }
                _hook.ThrowHook(_rodTipTransform, forcePercentage, _rodTipTransform.position += new Vector3(0, 0.15f, 0));
            }
        }

        private void DisableHook()
        {
            if (_hook != null && _hook.gameObject != null)
            {
                _hook.gameObject.SetActive(false);
            }
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

        }

        // caled when rod is deselected
        public void LastSelectExit()
        {
            StartCoroutine(ReturnToStartingPos());
        }

        private IEnumerator ReturnToStartingPos()
        {
            yield return new WaitForSeconds(0.75f);
            DisableHook();
            transform.SetPositionAndRotation(_startingPos, _startingRot);
        }
        public void ReelInHook()
        {
            if (_hook != null && _hook.inWater)
            {
                _hook.ReelInOneTick(_rodTipTransform);

                // distance is determined by mini game completion, ima leave this blank just for now

                //if (Vector3.Distance(_hook.transform.position, _rodTipTransform.position) > 0.75f)
                //{
                //    _hook.ReelInOneTick(_rodTipTransform);
                //}
                //else // Later on we can have it where if it gets close enough, it reels up! Or just have it destroy like this and then spawn in the trash
                //{
                //    _hook.ToggleInWater(false);
                //    DestroyHook();
                //}
            }
        }
    }
}
