using RMC.Mini.Controller.Commands;
using System;

namespace ZiercCode._DungeonGame.UI
{
    public class OpenCheckBoxCommand : ICommand
    {
        public string Message;
        public Action ConfirmCallback;
        public Action CancelCallback;

        public void OnSpawn()
        {
        }
    }
}