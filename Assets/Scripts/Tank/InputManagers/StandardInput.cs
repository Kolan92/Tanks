using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.InputMangers {

    public class StandardInput : IInputManager {
        public float Vertical { get { return Input.GetAxis(_movementAxisName); } }
        public float Horizontal { get { return Input.GetAxis(_turnAxisName); } }
        public bool FirePressed { get { return Input.GetButtonDown(_fireButton); } }

        private readonly string _movementAxisName;
        private readonly string _turnAxisName;
        private string _fireButton;

        public StandardInput(int playerNumber) {
            _movementAxisName = Constants.VerticalAxisName + playerNumber;
            _turnAxisName = Constants.HorizontalAxisName + playerNumber;
            _fireButton = Constants.Fire + playerNumber;
        }
    }
}
