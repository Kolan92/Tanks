using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public abstract class BaseWeaponController : BaseController, IWeaponController {
        protected Rigidbody Shell;

        protected BaseWeaponController(Rigidbody shell, Rigidbody rigidbody) : base(rigidbody) {
            Shell = shell;
        }
        public abstract void Update();

        public virtual void Shoot() {
            var position = Rigidbody.position + new Vector3(0, 2f, 0); //TODO Improve start position and rotation of the shell
            var shellInstance = UnityEngine.Object.Instantiate(Shell, position, Rigidbody.rotation) as Rigidbody;
            shellInstance.velocity = 30 * Rigidbody.transform.forward;
        }

        public virtual void ChangeWeapon() {
            throw new NotImplementedException();
        }
    }
}
