using RMC.Mini.Model;
using ZiercCode.Test.ObserverValue;

namespace ZiercCode.Test.MVC
{
    public class AddCalculatorModel : BaseModel
    {
        public readonly ObserverValue<int> A;
        public readonly ObserverValue<int> B;
        public readonly ObserverValue<int> Result;

        public AddCalculatorModel()
        {
            A = new ObserverValue<int>(0);
            B = new ObserverValue<int>(0);
            Result = new ObserverValue<int>(0);
        }
    }
}