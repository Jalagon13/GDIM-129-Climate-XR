using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public static class GameSignals
    {
        public static readonly Signal POWER_CHARGING = new("PowerCharging");
        public static readonly Signal POWER_RELEASED = new("PowerReleased");
        public static readonly Signal ROD_ACTIVATED = new("RodActived");
        public static readonly Signal ROD_DEACTIVATED = new("RodDeactived");
    }
}
