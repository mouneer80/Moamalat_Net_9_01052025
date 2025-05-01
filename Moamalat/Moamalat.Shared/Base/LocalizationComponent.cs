using Moamalat.Services;
using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Moamalat.Entities;

namespace Moamalat.Components
{
    public class LocalizationComponent : ComponentBase
    {
        [Inject]
        public NavigationManager? _navigationManager { get; set; }
        //[Inject]
        //protected IStringLocalizer _Localizer { get; set; }
        [Inject]
        protected IPlatformHandler _PlatformHandler { get; set; }
        [Inject]
        protected ISecureDataStoreService secureDataStoreService { get; set; }
        [Inject]
        protected IJSRuntime _Js { get; set; }
        [Parameter]
        public bool isModal { get; set; } = false;
        //[Parameter]
        public bool isMobileView { get; set; }

        public string? CurrentLanguage
        {
            get
            {
                if (string.IsNullOrEmpty(LocalizeService.Lang))
                    LocalizeService.Lang = "ar";
                return LocalizeService.Lang;
            }
            set
            {
                LocalizeService.Lang = value;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(CurrentLanguage))
            {
                await GetLanguage();


            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (string.IsNullOrEmpty(CurrentLanguage))
                    await ChangeLanguage("ar");

                await _Js.InvokeVoidAsync("Localize", LocalizeService.Lang);
                // await LocalizeAsync();
                //await _Js.InvokeVoidAsync("Localize");
                //  await ChangeLanguage(CurrentLanguage);
                ////  await _Js.InvokeVoidAsync("ChangeLang", CurrentLanguage);
            }
        }

        protected async Task LocalizeAsync()
        {
            await _Js.InvokeVoidAsync("Localize", LocalizeService.Lang);
            await GetLanguage();
        }
        protected async Task ChangeLanguage(string _Lang)
        {
            //CurrentLanguage = _Lang;
            LocalizeService.Lang = _Lang;
            await secureDataStoreService.SetAsync("Lang", LocalizeService.Lang);
            await _Js.InvokeVoidAsync("Localize", LocalizeService.Lang);
            StateHasChanged();
        }

        protected async Task GetLanguage()
        {
            LocalizeService.Lang = (await secureDataStoreService.GetAsync<string>("Lang")).Value;
            // CurrentLanguage = LocalizeService.Lang;
        }
        public string NumberToDate(string DateInNum)
        {

            if (DateInNum.Length < 8)
                return "Invalid Date";

            return $"{DateInNum.Substring(0, 4)}/{DateInNum.Substring(4, 2)}/{DateInNum.Substring(6, 2)}";

        }
       
        public void ShowLoading()
        {


        }

        public void HideLoading()
        {

        }

        protected async Task OnFileExceedsLimits()
        {
            string Message = "File Exceeds Limits....";
            string Okbtn = "Ok";
            string Caption = "Error";
            if (CurrentLanguage == "ar")
            {
                Message = "حجم الملف يزيد عن المسموح";
                Okbtn = "تم";
                Caption = "خطأ";
            }
            await _PlatformHandler.ShowAlert(Caption, Message, Okbtn, "");
        }
    }
}
