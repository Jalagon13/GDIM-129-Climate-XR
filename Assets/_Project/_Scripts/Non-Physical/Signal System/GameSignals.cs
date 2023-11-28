using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public static class GameSignals
    {
        // fishing rod signals
        public static readonly Signal POWER_CHARGING = new("PowerCharging");
        public static readonly Signal POWER_RELEASED = new("PowerReleased");
        public static readonly Signal ROD_ACTIVATED = new("RodActived");
        public static readonly Signal ROD_DEACTIVATED = new("RodDeactived");
        public static readonly Signal HOOK_RELEASED = new("HookReleased");

        // mini game signals
        public static readonly Signal REELING_IN = new("ReelingIn");
        public static readonly Signal NOT_REELING_IN = new("NotReelingIn");
        public static readonly Signal FISH_CAUGHT = new("FishCaught");
        public static readonly Signal FISH_GOT_AWAY = new("FishGotAway");

        // dialogue signals
        public static readonly Signal MAIN_DIALOGUE_STARTED = new("MainDialogueStarted");
        public static readonly Signal MAIN_DIALOGUE_FINISHED = new("MainDialogueFinished");
    }
}
