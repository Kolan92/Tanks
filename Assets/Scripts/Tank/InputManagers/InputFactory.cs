using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.InputMangers;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InputMangers {
    public class InputFactory : MonoBehaviour {
        public static InputFactory Instance;
        private VirtualJoystick _joystick;
        private IInputManager _inputManager;

        public static IInputManager GetInputManager(int playerNumber) {
            if (Application.platform == RuntimePlatform.Android)
                return Instance._joystick;
            return new StandardInput(playerNumber);
        }

        public void Awake() {

            _joystick = Object.FindObjectOfType<VirtualJoystick>();
            if (Application.platform != RuntimePlatform.Android) {
                var temp = _joystick.GetComponentInParent<Canvas>();
                temp.enabled = false;
                temp.gameObject.SetActive(false);
            }
            if (Instance == null)
                Instance = this;
            else if (Instance != null) {
                Destroy(gameObject);

            }
        }
    }
}
