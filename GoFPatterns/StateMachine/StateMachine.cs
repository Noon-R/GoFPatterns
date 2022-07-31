using System;
using System.Collections;
using System.Collections.Generic;

namespace Noon.StateMachine
{
    public sealed class StateMachine<TContext> 
    {
        private enum UpdateState { 
            Idle,
            Enter,
            Update,
            Exit,
        }

        public sealed class AnyState : StateBase
        {

            public override bool OnEnter()
            {
                return true;
            }

            public override bool OnExit()
            {
                return true;
            }

            public override StateBase Update()
            {
                return this;
            }
        }

        public abstract class StateBase
        {
            protected StateMachine<TContext> _ParentStateMachine = null;
            protected List<StateBase> _TransitionState = new List<StateBase>();

            internal StateBase() { }

            protected TContext Context { get {
                    if (_ParentStateMachine == null) {
                        return default(TContext);
                    }
                    return _ParentStateMachine.Context;
                } }

            public abstract bool OnEnter();
            public abstract bool OnExit();
            public abstract StateBase Update();

            internal void SetStatemachine(StateMachine<TContext> parent) {
                if (parent == null) {
                    return;
                }
                _ParentStateMachine = parent;
            }

            internal void AddTransitionState(StateBase nextState) {
                foreach (var state in _TransitionState) {
                    if (nextState.GetType() == state.GetType()) {
                        return;
                    }
                }
                _TransitionState.Add(nextState);
            }

            internal bool CanTransitionState<TState> ()
                where TState: StateBase {
                return CanTransitionState(typeof(TState));
            }

            internal bool CanTransitionState(Type type)
            {
                if (type == null) {
                    return false;
                }
                if (!typeof(StateBase).IsAssignableFrom(type)) {
                    return false;
                }
                foreach (var state in _TransitionState)
                {
                    if (state.GetType() == type)
                    {
                        return true;
                    }
                }
                return false;
            }

            protected StateBase GetTransitionState<TState>()
                where TState : StateBase
            {
                foreach (var state in _TransitionState)
                {
                    if (state.GetType() == typeof(TState))
                    {
                        return state;
                    }
                }
                return null;
            }

            protected StateBase GetTransitionState(Type type)
            {
                if (type == null)
                {
                    return null;
                }
                if (!typeof(StateBase).IsAssignableFrom(type))
                {
                    return null;
                }
                foreach (var state in _TransitionState)
                {
                    if (state.GetType() == type)
                    {
                        return state;
                    }
                }
                return null;
            }
        }

        public bool IsRunnnig => _CurrentState != null;
        public TContext Context => _Context;

        public StateBase CurrentState => _CurrentState;

        private List<StateBase> _StatesList = new List<StateBase>();
        private StateBase _CurrentState = null;
        private StateBase _NextState = null;
        private Stack<StateBase> _StateStack = new Stack<StateBase>();

        private TContext _Context;

        private UpdateState _CurrentUpdateState = UpdateState.Idle;
        private bool _CanTransition = true;

        public StateMachine(TContext context) {
            if (context == null) {
                throw new ArgumentNullException("Contextは必須です");
            }
            _Context = context;
            CreateState<AnyState>();
        }

        public void Update()
        {
            if (IsRunnnig)
            {

                switch (_CurrentUpdateState)
                {
                    case UpdateState.Enter:
                        if (_CurrentState.OnEnter())
                        {
                            _CurrentUpdateState = UpdateState.Update;
                            _CanTransition = true;
                        }
                        break;
                    case UpdateState.Update:
                        var nextState = _CurrentState.Update();
                        if (nextState != null)
                        {
                            if (_CurrentState.GetType() != nextState.GetType())
                            {
                                TransferState(nextState.GetType());
                            }
                        }
                        break;
                    case UpdateState.Exit:
                        if (_CurrentState.OnExit())
                        {
                            SwitchState();
                        }
                        break;
                    default:
                        break;
                }
            }
            else {
                if (_NextState == null) {
                    return;
                }
                SwitchState();
            }

        }

        private void SwitchState()
        {
            _CurrentState = _NextState;
            _CurrentUpdateState = UpdateState.Enter;
            _NextState = null;
        }

        #region BasicFSM
        public void SetStartState<TState>() 
            where TState : StateBase, new()
        {
            if (IsRunnnig) {
                return;
            }
            _NextState = GetState<TState>();
        }

        public void TransferState<TState>()
            where TState : StateBase, new()
        {
            if (!IsRunnnig) {
                return;
            }

            if (!_CanTransition) {
                return;
            }

            var canTransfer = _CurrentState.CanTransitionState<TState>();
            var IsAny2State = GetState<AnyState>().CanTransitionState<TState>();

            if (!canTransfer && !IsAny2State) {
                return;
            }
            PrePareNextState(GetState<TState>());
        }

        public void TransferState(Type type)
        {

            if (!IsRunnnig)
            {
                return;
            }

            if (!_CanTransition)
            {
                return;
            }

            if (!IsStateType(type))
            {
                return ;
            }

            var canTransfer = _CurrentState.CanTransitionState(type);
            var IsAny2State = GetState<AnyState>().CanTransitionState(type);

            if (!canTransfer && !IsAny2State)
            {
                return;
            }
            PrePareNextState(GetState(type));
        }

        /// <summary>
        /// 未設定のみ反映
        /// うわがきは禁止
        /// 実行後は遷移条件の追加を不可能に
        /// </summary>
        /// <param name="state"></param>
        public void AddState<TState>()
            where TState : StateBase,new ()
        {
            if (IsRunnnig) {
                return;
            }

            foreach (var state in _StatesList ) {
                if (state.GetType() == typeof(TState)) {
                    return;
                }
            }
            CreateState<TState>();

        }

        public void AddAnyTransitionState<TState> () 
            where TState : StateBase, new() {
            AddTransition<AnyState, TState>();
        }

        public void AddTransition<TPreState, TNextState>() 
            where TPreState : StateBase, new() 
            where TNextState : StateBase, new()
        {
            if (IsRunnnig) {
                return;
            }
            if (typeof(TPreState) == typeof(TNextState)) {
                return;
            }
            var preState = GetState<TPreState>();
            var nextState = GetState<TNextState>();

            preState.AddTransitionState(nextState);
        }

        public StateBase GetState<TState>()
        where TState : StateBase, new()
        {
            return GetState(typeof(TState));
        }

        public StateBase GetState(Type type)
        {
            if (!IsStateType(type)) {
                return null;
            }
            foreach (var state in _StatesList)
            {
                if (state.GetType() == type)
                {
                    return state;
                }
            }
            return CreateState(type);
        }

        #endregion

        #region Stack

        public void PushState()
        {
            if (!IsRunnnig) {
                return;
            }

            foreach (var state in _StateStack) {
                if (state.GetType() == _CurrentState.GetType()) {
                    return;
                }
            }

            _StateStack.Push(_CurrentState);
        }

        public void PopState() {
            if (!IsRunnnig)
            {
                return;
            }
            if (!_CanTransition) {
                return;
            }
            if (_StateStack.Count == 0) {
                return;
            }

            PrePareNextState(_StateStack.Pop());
        }

        public void DropStackState() {
            if (_StateStack.Count == 0) {
                return;
            }
            _StateStack.Pop();
        }

        public void ClearStack() {
            _StateStack.Clear();
        }

        #endregion

        private StateBase CreateState<TState>() 
            where TState : StateBase,new () {

            return CreateState(typeof(TState));
        }

        private StateBase CreateState(Type type)
        {
            if (!IsStateType(type)) {
                return null;
            }
            foreach (var state in _StatesList)
            {
                if (state.GetType() == type)
                {
                    return state;
                }
            }
            var newState = (StateBase)Activator.CreateInstance(type);
            newState.SetStatemachine(this);
            _StatesList.Add(newState);

            return newState;
        }

        private void PrePareNextState(StateBase state)
        {
            _NextState = state;
            _CurrentUpdateState = UpdateState.Exit;
            _CanTransition = false;
        }

        private bool IsStateType(Type type) {
            if (type == null)
            {
                return false;
            }
            if (!typeof(StateBase).IsAssignableFrom(type))
            {
                return false;
            }
            return true;
        }
    }
}
