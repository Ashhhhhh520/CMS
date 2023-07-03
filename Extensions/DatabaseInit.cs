using CMS.Models;
using System.Reflection;

namespace CMS.Extensions
{
    public class DatabaseInit
    {
        public static async Task OnDatabaseInit(IFreeSql freeSql)
        {
            var models = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.Namespace?.StartsWith("CMS.Models")??false);
            foreach (var model in models)
            {
                if (!freeSql.DbFirst.ExistsTable(model.Name))
                {
                    // add data tables
                    freeSql.CodeFirst.SyncStructure(model);

                    // add admin user
                    if (model.Name == nameof(users))
                    {
                        // add admin user
                        var admin = new users
                        {
                            Name = "admin",
                            Password = "admin",
                            UserName = "admin"
                        };
                        await freeSql.Insert(admin).ExecuteAffrowsAsync();
                    }
                }
            }
        }
    }
}
