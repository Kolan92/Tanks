using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class ComputerControler : BaseInputControler, IInputControler {
        public void Update() {
            throw new NotImplementedException();
        }

        public Vector3 GetMovment(float speed, Vector3 transform) {
            throw new NotImplementedException();
        }

        public Quaternion GetTurn(float turnSpeed, Quaternion rotation) {
            throw new NotImplementedException();
        }
    }
}
