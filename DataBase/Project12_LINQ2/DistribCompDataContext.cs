using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Project12_LINQ2
{
    [Database(Name = "DistribComp")]
    public partial class DistribCompDataContext : DataContext
    {
        private static readonly MappingSource mappingSource = new AttributeMappingSource();

        partial void OnCreated();

        public DistribCompDataContext(string connection)
            :
              base(connection, mappingSource)
        {
            OnCreated();
        }

        public DistribCompDataContext(IDbConnection connection)
            :
              base(connection, mappingSource)
        {
            OnCreated();
        }

        public DistribCompDataContext(string connection, MappingSource mappingSource)
            :
              base(connection, mappingSource)
        {
            OnCreated();
        }

        public DistribCompDataContext(IDbConnection connection, MappingSource mappingSource)
            :
              base(connection, mappingSource)
        {
            OnCreated();
        }

        public DistribCompDataContext()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)
        {
        }

        public Table<User> Users
        {
            get { return GetTable<User>(); }
        }

        public Table<Task> Tasks
        {
            get { return GetTable<Task>(); }
        }

        public Table<Payment> Payments
        {
            get { return GetTable<Payment>(); }
        }

       /* [Function(Name = "dbo.sp")]
        public ISingleResult<User> SP()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<User>)(result.ReturnValue));
        }*/
    }

    [Table(Name = "dbo.tblUsers")]
    public class User
    {
        [Column(IsPrimaryKey = true)]
        public int UserID { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public string Team { get; set; }
        [Column]
        public string Country { get; set; }
        [Column]
        public string Project { get; set; }
        [Column]
        public DateTime RegisterDate { get; set; }
        [Column]
        public double AvgScorePerDay { get; set; }
        [Column]
        public int AllScore { get; set; }
    }

    [Table(Name = "dbo.tblTasks")]
    public class Task
    {
        [Column(IsPrimaryKey = true)]
        public int TaskId { get; set; }
        [Column]
        public string TaskName { get; set; }
        [Column]
        public int TaskCost { get; set; }
        [Column]
        public DateTime TaskStartDate { get; set; }
        [Column]
        public DateTime TaskFinishDate { get; set; }
    }

    [Table(Name = "dbo.tblPayments")]
    public class Payment
    {
        [Column(IsPrimaryKey = true)]
        public int PaymentID { get; set; }
        [Column]
        [Association(Storage = "UserID", ThisKey = "UserID")]
        public int UserID { get; set; }
        [Column]
        [Association(Storage = "TaskID", ThisKey = "TaskID")]
        public int TaskID { get; set; }
        [Column]
        public DateTime PaymentDate { get; set; }
        [Column]
        public Decimal PaymentCredit { get; set; }
    }
}
