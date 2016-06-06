using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tank.Controllers {
    public class PlayerControler : BaseInputControler, IInputControler {
        private readonly string _movementAxisName;
        private readonly string _turnAxisName;
        private readonly string _fireButton;
        private bool _isFirePressed;

        public PlayerControler(int playerNumber) {
            _movementAxisName = Constants.VerticalAxisName + playerNumber;
            _turnAxisName = Constants.HorizontalAxisName + playerNumber;
            _fireButton = Constants.Fire + playerNumber;
        }

        public void Update() {
            _movementInputValue = Input.GetAxis(_movementAxisName);
            _turnInputValue = Input.GetAxis(_turnAxisName);
            _isFirePressed = Input.GetButtonDown(_fireButton);
        }

        public void Execute(float speed, float turnSpeed) {
            Move(speed);
            Turn(turnSpeed);
            Shoot();
        }

        private void Shoot() {
            if(!_isFirePressed) return;
            var position =  _Rigidbody.position + new Vector3(0, 2f, 0); //TODO Improve start position and rotation of the shell
            Rigidbody shellInstance = UnityEngine.Object.Instantiate(Shell, position, _Rigidbody.rotation) as Rigidbody;

            shellInstance.velocity = 30 * _Rigidbody.transform.forward;
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
