using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public abstract class BaseInputControler {
        protected float _movementInputValue;         
        protected float _turnInputValue;
        public Rigidbody _Rigidbody;
        public Rigidbody Shell;
    }
}
