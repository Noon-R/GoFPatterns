using System;
using System.Collections.Generic;
using System.Text;


namespace Noon.StateMachine.example
{
    public class SceneSwitcher
    {
        private StateMachine<SceneContext> _SceneStateMachine;

        public SceneSwitcher() {
            _SceneStateMachine = new StateMachine<SceneContext>(new SceneContext());
            _SceneStateMachine.AddState<LoadState>();
            _SceneStateMachine.AddState<TitleState>();
            _SceneStateMachine.AddState<InGameState>();

            _SceneStateMachine.AddTransition<LoadState, TitleState>();
            _SceneStateMachine.AddTransition<LoadState, InGameState>();

            _SceneStateMachine.AddAnyTransitionState<LoadState>();

            _SceneStateMachine.SetStartState<TitleState>();
        }

        public void update() {
            _SceneStateMachine.Update();

        }
    }

    public class SceneContext {
        public Type NextSceneType => _NextSceneType;
        private Type _NextSceneType = typeof(TitleState);

        public void SetNextScene<TState>()
                where TState : StateMachine<SceneContext>.StateBase
        {
            _NextSceneType = typeof(TState);
        }

    }

    public class TitleState : StateMachine<SceneContext>.StateBase
    {
        public override bool OnEnter()
        {
            Console.WriteLine("タイトル初期化");
            return true;
        }

        public override bool OnExit()
        {
            Console.WriteLine("タイトル終了処理");
            return true;
        }

        public override StateMachine<SceneContext>.StateBase Update()
        {
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.W) {
                Context.SetNextScene<InGameState>();
                return _ParentStateMachine.GetState<LoadState>();
            }
            Console.WriteLine("タイトル更新中");
            return this;
        }
    }

    public class LoadState : StateMachine<SceneContext>.StateBase
    {
        private int _ElapsedCount = 0;
        public LoadState():base()
        {
        }

        public override bool OnEnter()
        {
            Console.WriteLine("ロード初期化");
            _ElapsedCount = 0;
            return true;
        }

        public override bool OnExit()
        {
            Console.WriteLine("ロード終了処理");
            return true;
        }

        public override StateMachine<SceneContext>.StateBase Update()
        {
            _ElapsedCount++;
            Console.WriteLine($"ロード更新中:{_ElapsedCount}");

            if (_ElapsedCount > 10) {
                return _ParentStateMachine.GetState(Context.NextSceneType);
            }
            return this;
        }
    }

    public class InGameState : StateMachine<SceneContext>.StateBase
    {
        public override bool OnEnter()
        {
            Console.WriteLine("インゲーム初期化");
            return true;
        }

        public override bool OnExit()
        {
            Console.WriteLine("インゲーム終了処理");
            return true;
        }

        public override StateMachine<SceneContext>.StateBase Update()
        {
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.W)
            {
                Context.SetNextScene<TitleState>();
                return _ParentStateMachine.GetState<LoadState>();
            }
            Console.WriteLine("タイトル更新中");
            return this;
        }
    }
}
