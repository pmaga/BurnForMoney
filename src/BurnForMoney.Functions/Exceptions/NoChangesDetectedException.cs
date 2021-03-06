using System;

namespace BurnForMoney.Functions.Exceptions
{
    [Serializable]
    public class NoChangesDetectedException : InvalidOperationException
    {
        public NoChangesDetectedException()
            :base("Update operation must change at least one field. No changes detected.")
        {
            
        }
    }
}