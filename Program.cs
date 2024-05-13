using System;
using System.Collections.Generic;
using System.IO;

namespace FormFun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool loopMain = true;

            if(File.Exists(DataManager.savePath))
            {
                DataManager.LoadBinaryFromFile(DataManager.savePath);
            }

            while (loopMain)
            {
                State.SwitchState();
                string userInput = Console.ReadLine().ToLower();
                State.lastState = State.state;
                State.state = userInput;
                State.SwitchState();
            }
        }
    }
}
