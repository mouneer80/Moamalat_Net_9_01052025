using Microsoft.AspNetCore.Components;
using Moamalat.Entities;
using Moamalat.Services;

namespace Moamalat.AdminApp.Components.Employees
{
    public partial class Employees_Old : ComponentBase
    {
        [Inject]
        private PersonService _Employees_Old { get; set; }

        //private readonly NavigationManager _navigationManager;
        private IQueryable<Person> EmployeesList;

        protected override async Task OnInitializedAsync()
        {

            EmployeesList = (await _Employees_Old.GetAllPersons()).AsQueryable();
            // return base.OnInitializedAsync();
            StateHasChanged();
        }
        private void EditItem(int id)
        {
            // Handle edit logic here
        }

        private void DeleteItem(int id)
        {
            // Handle delete logic here
        }

        private void CreateNew()
        {
            // Navigate to Create page or show a modal for creating a new item
        }

        public bool alertisShown { get; set; }
        public string sResponse { get; set; }
        private void SaveButtonClicked()
        {
            alertisShown = true;
            //StateHasChanged();
        }
        private async Task GetAlertResponse(string value)
        {
            sResponse = value;
            alertisShown = false;

        }
    }
}
