using System;

namespace MySite.Model
{
    public class MySqlExtension
    {
        //public static DbContextOptionsBuilder UseFarmDatabase(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        //{
        //    string provider = configuration.GetConnectionString("DataProvider"), connection = configuration.GetConnectionString("ConnectionString");
        //    if (provider.Equals(DataBaseServer.SqlServer, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        return optionsBuilder.UseSqlServer(connection);
        //    }
        //    else if (provider.Equals(DataBaseServer.MySql, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        return optionsBuilder.UseMySql(connection);
        //    }
        //    else
        //    {
        //        throw Error.Argument("UseDatabaseServer", "No databaseProvider");
        //    }
        //}
    }
}