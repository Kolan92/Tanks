using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Tank.InputMangers {
    public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler , IDragHandler {
        public bool IsPressed;

        private Button _button;
        void Start() {
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData) {
            IsPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            IsPressed = false;
        }

        public void OnDrag(PointerEventData eventData) {
            IsPressed = true;
        }
    }
}
