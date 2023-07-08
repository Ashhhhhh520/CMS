using CMS.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace CMS.Extensions
{
    public class DatabaseInit
    {
        public static async Task OnDatabaseInit(IFreeSql freeSql,WebApplicationBuilder builder)
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

            var contents_file = new FileInfo(Path.Combine(builder.Environment.WebRootPath, "data","contents.json"));
            var menus_file = new FileInfo(Path.Combine(builder.Environment.WebRootPath, "data","menus.json"));
            if (contents_file.Exists)
            {
                if(! await freeSql.Select<contents>().AnyAsync())
                {
                    var contents = JsonConvert.DeserializeObject<List<contents>>(await File.ReadAllTextAsync(contents_file.FullName));
                    await freeSql.Insert(contents).ExecuteAffrowsAsync();
                }
            }

            if (menus_file.Exists)
            {
                if(!await freeSql.Select<menus>().AnyAsync())
                {
                    var contents = JsonConvert.DeserializeObject<List<menus>>(await File.ReadAllTextAsync(contents_file.FullName));
                    await freeSql.Insert(contents).ExecuteAffrowsAsync();
                }
            }
        }
    }
}
