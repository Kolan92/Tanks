using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using InputMangers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tank.Controllers {
    public class PlayerMovmentControler : BaseInputControler, IMovmentControler {
        private readonly IInputManager _input;

        public PlayerMovmentControler(int playerNumber, Rigidbody rigidbody, Rigidbody shell) 
            : base(rigidbody) {
            _input = InputFactory.GetInputManager(playerNumber);
            _weaponController = new PlayerWeaponController(playerNumber, shell, rigidbody);
        }

        public void Update() {
            _movementInputValue = _input.Vertical;
            _turnInputValue = _input.Horizontal;
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
