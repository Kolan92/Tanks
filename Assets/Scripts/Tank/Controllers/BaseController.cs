using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public abstract class BaseController {
        public Rigidbody Rigidbody;

        protected BaseController(Rigidbody rigidbody) {
            Rigidbody = rigidbody;
        }
    }
}
