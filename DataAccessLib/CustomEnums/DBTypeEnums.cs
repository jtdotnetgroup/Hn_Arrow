namespace DataAccess.CustomEnums
{
    public enum DBTypeEnums
    {
        SQLSERVER,
        MYSQL,
        ORACLE,
        /// <summary>
        /// 默认类别，MYSQL和MSSQL主要用于查询参数标识符，MYSQL和SQLSERVER用@，ORACLE用 ：
        /// </summary>
        Default

        
    }
}