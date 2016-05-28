using System;
using UnityEngine;

namespace Assets.Scripts.Tank.DerivedClasses {
    public class PlayerTankMovment : TankMovement, ITankMovement {
        private TankMovement m_Movement;

        public PlayerTankMovment(TankMovement m_Movement) {
            this.m_Movement = m_Movement;
        }

        public void FixedUpdate() {
            if (PlayerNumber != 1)return;
            Move();
            Turn();
        }

        //public int PlayerNumber {
        //    get { return playerNumber; }
        //    set { playerNumber = value; }
        //}

        public void Move() {
            var movement = transform.forward * _movementInputValue * Speed * Time.deltaTime;

            _rigidbody.MovePosition(_rigidbody.position + movement);
        }

        public void Turn() {
            var turn = _turnInputValue * TurnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);

            _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
        }

        private void Update() {
            if(PlayerNumber == 0) return;
            _movementInputValue = Input.GetAxis(_movementAxisName);
            _turnInputValue = Input.GetAxis(_turnAxisName);
        }
    }
}
