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

        public Vector3 GetMovment(float speed, Vector3 transform) {
            return transform * _movementInputValue * speed * Time.deltaTime;;
        }

        public Quaternion GetTurn(float turnSpeed) {
            var turn = _turnInputValue * turnSpeed * Time.deltaTime;
            return Quaternion.Euler(0f, turn, 0f);
        }
    }
}
