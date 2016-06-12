using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tank.Enums {
    public enum Distance {
        TooFar = 1,
        TooClose = 2,
        InBeetween = 4,
        CloseToFight = 8
    }
}
