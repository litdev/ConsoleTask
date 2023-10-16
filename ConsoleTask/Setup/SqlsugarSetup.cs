using Furion.Logging.Extensions;

namespace ConsoleTask
{
    /// <summary>
    /// 使用SqlSugar
    /// </summary>
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var configConnection = new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.SqlServer,
                ConnectionString = configuration["ConnectionString"],
                IsAutoCloseConnection = true,
            };

            SqlSugarScope sqlSugar = new SqlSugarScope(configConnection,
                db =>
                {
                    //单例参数配置，所有上下文生效
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        $"Sql:\r\n\r\n {UtilMethods.GetSqlString(DbType.SqlServer, sql, pars)}".LogInformation();
                    };
                });

            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton

        }
    }
}