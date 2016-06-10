using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public abstract class BaseInputControler : BaseController{
        protected IWeaponController _weaponController;
        protected float _movementInputValue;         
        protected float _turnInputValue;

        protected BaseInputControler(Rigidbody rigidbody)
            : base(rigidbody) {
        }
    }
}
