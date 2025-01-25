using RMC.Mini.Controller.Commands;
using System;
using ZiercCode.Test.Reference;

namespace ZiercCode.Test.MVC
{
    public class OpenCheckBoxCommand : ICommand, IReference
    {
        public string Message;
        public Action ConfirmCallback;
        public Action CancelCallback;

        public void OnSpawn()
        {
        }
    }
}