using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tank.Interfaces {
    public interface IWeaponController {
        void Update();
        void Shoot();
        void ChangeWeapon();
    }
}
