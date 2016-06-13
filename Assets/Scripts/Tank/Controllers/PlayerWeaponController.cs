using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using InputMangers;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class PlayerWeaponController : BaseWeaponController {
        private IInputManager _input;
        private bool _isFirePressed;

        public PlayerWeaponController(int playerNumber, Rigidbody shell, Rigidbody rigidbody) 
            : base(shell, rigidbody) {
            _input = InputFactory.GetInputManager(playerNumber);
        }

        public override void Update() {
            _isFirePressed = _input.FirePressed;
        }

        public override void Shoot() {
            if(!_isFirePressed) return;
            base.Shoot();
        }
    }
}
