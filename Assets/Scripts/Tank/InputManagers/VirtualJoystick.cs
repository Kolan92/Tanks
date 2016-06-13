using UnityEngine;
using System.Collections;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.Tank.InputMangers;

namespace Assets.Scripts.Tank.InputMangers {
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IInputManager {

        public float Vertical {
            get { return _input.z; }
        }

        public float Horizontal {
            get { return _input.x; }
        }

        public bool FirePressed { get { return _fire.IsPressed; } }

        private Image _background;
        private Image _joystick;
        private Vector3 _input;

        private FireButton _fire;

        void Start() {
            _background = GetComponent<Image>();
            _joystick = transform.GetChild(0).GetComponent<Image>();
            var goParent = transform.parent.gameObject;
            _fire = goParent.GetComponentInChildren<FireButton>(); //TODO 
        }

        public void OnDrag(PointerEventData eventData) {
            Vector2 position;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_background.rectTransform, eventData.position,
                eventData.pressEventCamera, out position)) {
                position.x = position.x / _background.rectTransform.sizeDelta.x;
                position.y = position.y / _background.rectTransform.sizeDelta.y;

                _input = new Vector3(position.x * 2 + 1, 0, position.y * 2 - 1);
                _input = _input.magnitude > 1 ? _input.normalized : _input;

                _joystick.rectTransform.anchoredPosition = new Vector3(
                    _input.x * _background.rectTransform.sizeDelta.x / 3,
                    _input.z * _background.rectTransform.sizeDelta.y / 3);
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData) {
            _input = Vector3.zero;
            _joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}