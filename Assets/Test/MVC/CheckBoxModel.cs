using RMC.Core.Architectures.Mini.Model;
using System;
using ZiercCode.Test.ObserverValue;

namespace ZiercCode.Test.MVC
{
    public class CheckBoxModel : BaseModel
    {
        public readonly ObserverValue<string> MessageText;
        public readonly ObserverValue<Action> ConfirmAction;
        public readonly ObserverValue<Action> CancelAction;

        public CheckBoxModel()
        {
            MessageText = new ObserverValue<string>("");
            ConfirmAction = new ObserverValue<Action>(null);
            CancelAction = new ObserverValue<Action>(null);
        }
    }
}