using BlazorComponent;
using CMS.Models;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace CMS.Pages.AdminPages
{
    public partial class Menu
    {
        [Inject]
        IFreeSql FreeSql { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        bool EditDialog { get; set; }

        menus MenuEditor { get; set; } = new menus();

        List<DataTableHeader<menus>> Headers { get; set; } = new List<DataTableHeader<menus>>()
        {
            new (){ Text="",Value="data-table-expand"},
            new DataTableHeader<menus>{Text="Menu",Value="Menu",Sortable=false,Filterable=false},
            new DataTableHeader<menus>{Text="Sort",Value="Sort"},
            new (){ Text= "Actions", Value= "actions",Sortable=false,Width="100px",Align=DataTableHeaderAlign.Center, }
        };

        List<menus> Items { get; set; }=new List<menus>();

        protected override async Task OnInitializedAsync()
        {
            await QueryMenus();
        }

        async Task QueryMenus()
        {
            Items = await FreeSql.Select<menus>()
                .Where(a => true)
                .OrderBy(a => a.Sort)
                .ToListAsync();
            await InvokeAsync(StateHasChanged);
        }

        void OnEditContent(int id) => NavigationManager.NavigateTo($"/admin/content/{id}");

        void OnEditMenu(menus selector)
        {
            MenuEditor = selector;
            EditDialog = true;
        }

        async Task OnSaveMenu()
        {
            await FreeSql.InsertOrUpdate<menus>().SetSource(MenuEditor).ExecuteAffrowsAsync();
            EditDialog = false;
            await QueryMenus();
        }

        async Task OnDeleteMenu(menus selector)
        {
            await FreeSql.Delete<menus>().Where(selector).ExecuteAffrowsAsync();
            await QueryMenus();
        }
    }
}
