using System;
using System.Collections.Generic;
using System.Text;
using Noon.StateMachine.example;
using System.Threading.Tasks;
using System.Threading;

namespace GoFPatterns
{
    class App
    {
        static void Main(string[] args)
        {
            MainLoop();
        }

        public static async void MainLoop() {
            SceneSwitcher sceneSwitcher = new SceneSwitcher();

            while (true)
            {
                sceneSwitcher.update();
                Thread.Sleep(100);
            }
        }
    }
}
