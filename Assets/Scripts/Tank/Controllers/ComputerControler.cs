using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.AI;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class ComputerControler : BaseInputControler, IInputControler {
        private IEnemyState _currentState;

        public ComputerControler() {
            _currentState = new IdleState();
            
        }
        public void Update() {
            _currentState.UpdateState();
        }

        public Vector3 GetMovment(float speed, Vector3 transform) {
            throw new NotImplementedException();
        }

        public Quaternion GetTurn(float turnSpeed) {
            throw new NotImplementedException();
        }
    }
}
