using System;

namespace Project12_LINQ1
{
    class Payment
    {
        public uint ID;
        public uint UserID;
        public uint TaskID;
        public DateTime Date;
        public uint Credit;

        public Payment(uint ID, uint UserID, uint TaskID, DateTime Date, uint Credit)
        {
            this.ID = ID;
            this.UserID = UserID;
            this.TaskID = TaskID;
            this.Date = Date;
            this.Credit = Credit;
        }
    }
}
