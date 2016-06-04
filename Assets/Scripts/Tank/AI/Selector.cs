using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tank.AI {
    class Selector : List<IEnemyState> {
        public bool Execute() {
            foreach (var state in this) {
                while (state.Status == StateStatus.Running) {
                    state.UpdateState();
                }
                if (state.Status == StateStatus.Sucess)
                    return true;
            }
            return false;
        }
    }

    class Sequence : List<IEnemyState> {
        public bool Execute() {
            foreach (var state in this) {
                while (state.Status == StateStatus.Running) {
                    state.UpdateState();
                }
                if (state.Status == StateStatus.Failed)
                    return false;
            }
            return true;
        }
    }
}
