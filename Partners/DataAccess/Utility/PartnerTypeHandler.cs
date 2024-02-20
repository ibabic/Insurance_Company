using Dapper;
using Partners.DataAccess.Models;
using System;
using System.Data;

namespace Partners.DataAccess.Utility
{
    public class PartnerTypeHandler : SqlMapper.TypeHandler<PartnerType>
    {
        public override PartnerType Parse(object value)
        {
            return (PartnerType)Enum.Parse(typeof(PartnerType), value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, PartnerType value)
        {
            parameter.Value = (int)value;
        }
    }
}