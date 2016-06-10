using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class PlayerWeaponController : BaseWeaponController{
        private readonly string _fireButton;
        private bool _isFirePressed;

        public PlayerWeaponController(int playerNumber, Rigidbody shell, Rigidbody rigidbody) 
            : base(shell, rigidbody) {
            _fireButton = Constants.Fire + playerNumber;
        }

        public override void Update() {
            _isFirePressed = Input.GetButtonDown(_fireButton);
        }

        public override void Shoot() {
            if(!_isFirePressed) return;
            base.Shoot();
        }
    }
}
