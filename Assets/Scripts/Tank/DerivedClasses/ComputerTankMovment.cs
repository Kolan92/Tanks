using UnityEngine;

namespace Assets.Scripts.Tank.DerivedClasses {
    public class ComputerTankMovment : TankMovement, ITankMovement {
        //private Transform myTransform;
        //private Transform target;
        private int maxDistance = 100;
        private int minDistance = 20;
        private int distanceMargin = 2;

        public void Move() {
            var movement = transform.forward * _movementInputValue * Speed * Time.deltaTime;

            _rigidbody.MovePosition(_rigidbody.position + movement);
        }

        public void Turn() {
            var turn = _turnInputValue * TurnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);

            _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
        }

        public void FixedUpdate() {
            if (PlayerNumber != 0) return;
            SpotEnemy();
            //Move();
            //Turn();
        }

        //public int PlayerNumber {
        //    get { return playerNumber; }
        //    set { playerNumber = value; }
        //}

        private void Update() {
            _movementInputValue = 1f;
            _turnInputValue = 0.1f;
        }

        private void SpotEnemy() {
            var go = GameObject.FindGameObjectWithTag("Player");
            var target = go.transform;

            Debug.DrawLine(target.position, _rigidbody.transform.position, Color.red);


            _rigidbody.transform.rotation = Quaternion.Slerp(_rigidbody.transform.rotation,
                Quaternion.LookRotation(target.position - _rigidbody.transform.position), TurnSpeed * Time.deltaTime);
            var distance = Vector3.Distance(target.position, _rigidbody.transform.position);
            if (distance > maxDistance) return;
            if (distance < minDistance - distanceMargin) {
                //Move towards target
                _rigidbody.transform.position += _rigidbody.transform.forward * Speed * Time.deltaTime *-1;
                //Move();
            }
            else if(distance > minDistance + distanceMargin) {
                _rigidbody.transform.position += _rigidbody.transform.forward * Speed * Time.deltaTime;
            }
        }
    }
}
