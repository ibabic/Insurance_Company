namespace Partners.DataAccess.Utility
{
    public static class StoredProcedures
    {
        public const string Partner_GetAll = "dbo.spPartner_GetAll";
        public const string Partner_Get = "dbo.spPartner_Get";
        public const string Partner_Insert = "dbo.spPartner_Insert";
        public const string Partner_Delete = "dbo.spPartner_Delete";
        public const string Partner_SoftDelete = "dbo.spPartner_SoftDelete";
        public const string Partner_Update = "dbo.spPartner_Update";
        public const string Policy_Get = "dbo.spPolicy_Get";
        public const string Policy_GetByPartnerId = "dbo.spPolicy_GetByPartnerId";
        public const string Policy_GetAll = "dbo.spPolicy_GetAll";
        public const string Policy_Insert = "dbo.spPolicy_Insert";
        public const string Policy_Update = "dbo.spPolicy_Update";
        public const string Policy_Delete = "dbo.spPolicy_Delete";
        public const string Policy_SoftDelete = "dbo.spPolicy_SoftDelete";
    }
}