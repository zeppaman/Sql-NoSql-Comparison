using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SNS.Benchmark.Runner.Sql.Entities;

/// <summary>
/// This namespace contains all nhibernate class mappings
/// </summary>
namespace SNS.Benchmark.Runner.Entities
{
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("Category");
            Schema("dbo");

            Id(x => x.CategoryId, map => { map.Generator(Generators.Guid); });
            Property(x => x.Name);
        }

    }


    public class MasterMap : ClassMapping<Master>
    {
        public MasterMap()
        {
            Table("Master");
            Schema("dbo");

            Id(x => x.MasterId);
            Property(x => x.CategoryId);
            Property(x => x.MasterField0);
            Property(x => x.MasterField1);
            Property(x => x.MasterField2);
            Property(x => x.MasterField3);
            Property(x => x.FieldToSum);

        }

    }


    public class DetailMap : ClassMapping<Detail>
    {
        public DetailMap()
        {
            Table("Detail");
            Schema("dbo");

            Id(x => x.DetailId);
            Property(x => x.MasterId);
            Property(x => x.DetailField0);
            Property(x => x.DetailField1);
            Property(x => x.DetailField2);
            Property(x => x.DetailField3);


            Property(x => x.FieldToSum);

        }

    }
}