using System.Collections;
using System.Collections.Generic;

namespace Noon.StateMachine
{
    public interface IState<T>
    {
        void Update();
        void OnEnter();
        void OnExit();

    }

    public sealed class EmptyState<T> : IState<T>
    {
       
        public void Update() { }
        public void OnEnter() { }
        public void OnExit() { }
    }

}