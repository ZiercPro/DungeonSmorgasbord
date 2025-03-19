using RMC.Core.Observables;
using RMC.Mini.Model;
using System;

namespace ZiercCode._DungeonGame.UI.CheckBox
{
    public class CheckBoxModel : BaseModel
    {
        public Observable<string> MessageText;
        public Action ConfirmAction;
        public Action CancelAction;

        public CheckBoxModel()
        {
            MessageText = new Observable<string>();
        }
    }
}