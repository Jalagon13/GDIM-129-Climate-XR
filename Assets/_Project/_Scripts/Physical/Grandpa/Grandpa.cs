using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class Grandpa : MonoBehaviour
    {
        private static readonly int _fishCaughtMax = 10;

        private int _fishCounter;

        private void Awake()
        {
            GameSignals.FISH_CAUGHT.AddListener(AdvanceGameState);
        }

        private void OnDestroy()
        {
            GameSignals.FISH_CAUGHT.RemoveListener(AdvanceGameState);
        }

        private void AdvanceGameState(ISignalParameters parameters)
        {
            // when the player catches a fish, we can have Grandpa playout a sequence of dialogue depending on this _fishCounter variable
            // I assume the fish caught and therefore dialogue is sequential so we can use this fishCounter as a proxy for what dialogue to play.
            _fishCounter++; 

            if(_fishCounter >= _fishCaughtMax)
            {
                // Once player has caught _fishCaughtMax fish, the game has ended
            }
        }
    }
}
