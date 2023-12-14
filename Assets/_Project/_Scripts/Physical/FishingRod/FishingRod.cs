using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MagnetFishing
{

    [Serializable]
    public class Fish
    {
        public string Name;
    }

    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private Hook _hook;
        [SerializeField] private Transform _rodTipTransform;
        [SerializeField] private AudioClip _rodGrip;
        [SerializeField] private AudioClip _rodLetGo;
        [SerializeField] private List<Fish> uncaughtFishes = new List<Fish>();

        private Dictionary<string, int> caughtFishes = new Dictionary<string, int>();
        private Fish currentFish;
        public int fishiesCaughtCounter = 0;

        private Vector3 _startingPos;
        private Quaternion _startingRot;


        // Public method to get the caught fishes
        public Dictionary<string, int> GetCaughtFishes()
        {
            return caughtFishes;
        }

        private void Awake()
        {
            _startingPos = transform.position;
            _startingRot = transform.rotation;
        }

        private void OnEnable()
        {
            DisableHook();
            transform.SetPositionAndRotation(_startingPos, _startingRot);

            GameSignals.HOOK_RELEASED.AddListener(ThrowHook);
            GameSignals.FISH_CAUGHT.AddListener(FishCaught);
            GameSignals.FISH_GOT_AWAY.AddListener(FishGotAway);
        }

        private void OnDisable()
        {
            DisableHook();

            GameSignals.HOOK_RELEASED.RemoveListener(ThrowHook);
            GameSignals.FISH_CAUGHT.RemoveListener(FishCaught);
            GameSignals.FISH_GOT_AWAY.RemoveListener(FishGotAway);
        }

        private void Start()
        {
            DisableHook();
            SelectFish(); // Select the first fish
        }

        private void FishCaught(ISignalParameters parameters)
        {
            if (currentFish != null)
            {
                // Remove from uncaught and add/update in caught
                uncaughtFishes.Remove(currentFish);
                if (caughtFishes.ContainsKey(currentFish.Name))
                {
                    caughtFishes[currentFish.Name]++;
                }
                else
                {
                    caughtFishes.Add(currentFish.Name, 1);
                }

                UnityEngine.Debug.Log(currentFish.Name + " Caught!");
            }
            DisableHook();
            // increment fishes caught.
            if (fishiesCaughtCounter == uncaughtFishes.Count-1)
            {
                fishiesCaughtCounter = 0;
            }
            else
            {
                fishiesCaughtCounter++;
            }
            //
            SelectFish(); // Select a new fish
        }

        private void FishGotAway(ISignalParameters parameters)
        {
            UnityEngine.Debug.Log(currentFish != null ? currentFish.Name + " Got Away!" : "Fish Got Away!");
            DisableHook();
            /*SelectFish(); // Select a new fish*/
            // REMOVED THE ABOVE LINE BECAUSE WE DON'T WANT TO SELECT A NEW FISH. WE WANT TO GO IN THE PROPER ORDER.
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

        private void DisableHook(ISignalParameters parameters = null)
        {
            if (_hook != null && _hook.gameObject != null)
            {
                _hook.gameObject.SetActive(false);
            }
        }

        // called when front trigger is pressed
        public void Activate(ActivateEventArgs args = null)
        {
            GameSignals.ROD_ACTIVATED.Dispatch();
        }

        // called when front trigger is released
        public void DeActivate(DeactivateEventArgs args = null)
        {
            //GameSignals.ROD_DEACTIVATED.Dispatch();
        }

        // called when rod is selected
        public void FirstSelectEnter(SelectEnterEventArgs args)
        {
            //GameSignals.ROD_SELECTED.Dispatch();
            GameSignals.ROD_SELECTED.Dispatch();
            MMSoundManagerSoundPlayEvent.Trigger(_rodGrip, MMSoundManager.MMSoundManagerTracks.Sfx, transform.position);
        }

        // caled when rod is deselected
        public void LastSelectExit()
        {
            GameSignals.ROD_DESELECTED.Dispatch();
            StartCoroutine(ReturnToStartingPos());
        }

        private IEnumerator ReturnToStartingPos()
        {
            GameSignals.ROD_DESELECTED.Dispatch();

            yield return new WaitForSeconds(0.75f);
            MMSoundManagerSoundPlayEvent.Trigger(_rodLetGo, MMSoundManager.MMSoundManagerTracks.Sfx, transform.position);
            DisableHook();
            transform.SetPositionAndRotation(_startingPos, _startingRot);
        }
        public void ReelInHook()
        {
            if (_hook != null)
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

        private void SelectFish()
        {
            UnityEngine.Debug.Log($"Current fish is {currentFish}");
            if (uncaughtFishes.Count > 0)
            {
                /*int index = UnityEngine.Random.Range(0, uncaughtFishes.Count);
                currentFish = uncaughtFishes[index];*/

                // CURRENTLY ISN'T RANDOM, SO WE'LL INCREMENT ONE BY ONE FOR NOW.
                UnityEngine.Debug.Log($"fishiesCaughtCounter is {fishiesCaughtCounter}");
                currentFish = uncaughtFishes[fishiesCaughtCounter];
                UnityEngine.Debug.Log($"Next fish is {currentFish}");
            }
        }
        //debug only, display all fish that caught
        private void DisplayCaughtFishes()
        {
            foreach (var pair in caughtFishes)
            {
                UnityEngine.Debug.Log($"Caught {pair.Key}: {pair.Value} times.");
            }
        }
    }
}
