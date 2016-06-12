using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public abstract class BaseWeaponController : BaseController, IWeaponController {
        protected Rigidbody Shell;
        protected BackgroundWorker BackgroundWorker;

        protected BaseWeaponController(Rigidbody shell, Rigidbody rigidbody) : base(rigidbody) {
            Shell = shell;
            SetupWoker();
        }
        public abstract void Update();

        public virtual void Shoot() {

            if (BackgroundWorker.IsBusy) return;
            var position = Rigidbody.position + Rigidbody.rotation * new Vector3(0, 1.6f, 1); //TODO Improve start position and rotation of the shell
            var rotation = Rigidbody.rotation;// + new Quaternion(-15, 0, 0, 0);
            rotation.x = - 15;
            var shellInstance = UnityEngine.Object.Instantiate(Shell, position, Rigidbody.rotation) as Rigidbody;
            shellInstance.gameObject.SetActive(true);
            shellInstance.velocity = 50 * Rigidbody.transform.forward;
            BackgroundWorker.RunWorkerAsync();
        }

        public virtual void ChangeWeapon() {
            throw new NotImplementedException();
        }

        private void SetupWoker() {
            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += WaitFunction;
        }

        private void WaitFunction(object sender, DoWorkEventArgs e) {
            Thread.Sleep(500);
        }
    }
}
