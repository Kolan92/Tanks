using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class ComputerWeaponController :BaseWeaponController{
        public ComputerWeaponController(Rigidbody shell, Rigidbody rigidbody)
            : base(shell, rigidbody) {
        }

        public override void Update() {
            throw new NotImplementedException();
        }
    }
}
