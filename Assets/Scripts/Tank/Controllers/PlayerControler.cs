using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class PlayerControler : BaseInputControler, IInputControler {
        private readonly string _movementAxisName;
        private readonly string _turnAxisName;

        public PlayerControler(int playerNumber) {
            _movementAxisName = Constants.VerticalAxisName + playerNumber;
            _turnAxisName = Constants.HorizontalAxisName + playerNumber;
        }

        public void Update() {
            _movementInputValue = Input.GetAxis(_movementAxisName);
            _turnInputValue = Input.GetAxis(_turnAxisName);
        }

        public void Execute(float speed, float turnSpeed) {
            Move(speed);
            Turn(turnSpeed);
        }

        private void Move(float speed) {
            var movement = _Rigidbody.transform.forward * _movementInputValue * speed * Time.deltaTime;
            _Rigidbody.MovePosition(_Rigidbody.position + movement);
        }

        private void Turn(float turnSpeed) {
            var turn = _turnInputValue * turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);
            _Rigidbody.MoveRotation(_Rigidbody.rotation * turnRotation);
        }
    }
}
