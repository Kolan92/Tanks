using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tank.Controllers {
    public class PlayerInputControler : BaseInputControler, IInputControler {
        private readonly string _movementAxisName;
        private readonly string _turnAxisName;

        public PlayerInputControler(int playerNumber, Rigidbody rigidbody, Rigidbody shell) 
            : base(rigidbody){
            _movementAxisName = Constants.VerticalAxisName + playerNumber;
            _turnAxisName = Constants.HorizontalAxisName + playerNumber;
            _weaponController = new PlayerWeaponController(playerNumber, shell, rigidbody);
        }

        public void Update() {
            _movementInputValue = Input.GetAxis(_movementAxisName);
            _turnInputValue = Input.GetAxis(_turnAxisName);
            _weaponController.Update();
        }

        public void Execute(float speed, float turnSpeed) {
            Move(speed);
            Turn(turnSpeed);
            _weaponController.Shoot();
        }

        private void Move(float speed) {
            var movement = Rigidbody.transform.forward * _movementInputValue * speed * Time.deltaTime;
            Rigidbody.MovePosition(Rigidbody.position + movement);
        }

        private void Turn(float turnSpeed) {
            var turn = _turnInputValue * turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);
            Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
        }
    }
}
