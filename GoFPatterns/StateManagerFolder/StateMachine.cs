using System.Collections;
using System.Collections.Generic;

namespace Noon.StateMachine
{
    public sealed class StateMachine<TKey,T> where TKey : struct
    {
        Dictionary<TKey, IState<T>> _StatesMap;
        IState<T> _CurrentState;
        TKey _CurrentKey;
        TKey _PreKey;

        public StateMachine() {
            if (!IsEnum()) { 
                
            }

            _StatesMap = new Dictionary<TKey, IState<T>>();
            _CurrentState = new EmptyState<T>();
        }

        public void Update()
        {
            _CurrentState.Update();
        }

        
        public void Add(TKey key, IState<T> state)
        {
            if (_StatesMap.ContainsKey(key))
            {
                //未設定のみ反映
                //うわがきは禁止
                return;
            }
            _StatesMap.Add(key, state);

        }

        private void Change(TKey key)
        {
            if (_StatesMap.ContainsKey(key))
            {
                return;
            }

            _CurrentState.OnExit();
            _CurrentKey = key;
            _CurrentState = _StatesMap[key];
            _CurrentState.OnEnter();
        }

        public void Remove(TKey key)
        {
            if (!_StatesMap.ContainsKey(key)) {
                return;
            }

            _StatesMap.Remove(key);
        }

        private bool IsEnum() {
            System.Type type = typeof(TKey);
            return type.IsEnum;
        }
    }
}
