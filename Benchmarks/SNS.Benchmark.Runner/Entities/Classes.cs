using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

/// <summary>
/// This namespace contains all entities
/// </summary>
namespace SNS.Benchmark.Runner.Sql.Entities
{
    public class Category
    {
        [BsonId]
        public virtual Guid CategoryId {get;set;}
        public virtual string Name { get; set; }
    }


    public class Detail
    {
        [BsonId]
        public virtual Guid DetailId { get; set; }
        public virtual Guid MasterId { get; set; }

        public virtual string DetailField0 { get; set; }
        public virtual string DetailField1 { get; set; }
        public virtual string DetailField2 { get; set; }
        public virtual string DetailField3 { get; set; }


        public virtual long FieldToSum { get; set; }

    }

    public class Master
    {
        public virtual Guid CategoryId { get; set; }
        [BsonId]
        public virtual Guid MasterId { get; set; }



        public virtual string MasterField0 { get; set; }
        public virtual string MasterField1 { get; set; }
        public virtual string MasterField2 { get; set; }
        public virtual string MasterField3 { get; set; }

        public virtual long FieldToSum { get; set; }

    }
}
