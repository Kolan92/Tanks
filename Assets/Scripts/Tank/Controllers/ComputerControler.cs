using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;

namespace Assets.Scripts.Tank.Controllers {
    public class ComputerControler : ITankControler {
        public TankMovement TankMovement { get; set; }
        public TankShooting TankShooting { get; set; }

        public void UpdateTank() {
            throw new NotImplementedException();
        }
    }
}
