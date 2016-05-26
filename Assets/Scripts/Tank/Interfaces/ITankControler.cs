using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tank.Interfaces {
    public interface ITankControler {
        TankMovement TankMovement { get; }
        TankShooting TankShooting { get; }

        void UpdateTank();
    }
}
