using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Enums;

namespace Assets.Scripts {
    public static class Dispatcher {
        public delegate void GameEvent(object @object = null);
        private static readonly IDictionary<GameEventEnum, GameEvent> _gameEventDictionary = new Dictionary<GameEventEnum, GameEvent>();

        public static void AddListener(GameEventEnum eventEnum, GameEvent eventFunction) {
            if (_gameEventDictionary.ContainsKey(eventEnum))
                _gameEventDictionary[eventEnum] += eventFunction;
            else 
                _gameEventDictionary.Add(eventEnum, eventFunction);
        }

        public static void RemoveListener(GameEventEnum eventEnum, GameEvent eventFunction) {
            if (_gameEventDictionary.ContainsKey(eventEnum))
                _gameEventDictionary[eventEnum] -= eventFunction;
        }

        public static void Dispatch(GameEventEnum evntEnum, object @object = null) {
            if (_gameEventDictionary.ContainsKey(evntEnum) && _gameEventDictionary[evntEnum] != null)
                _gameEventDictionary[evntEnum](@object);
        }
    }
}
