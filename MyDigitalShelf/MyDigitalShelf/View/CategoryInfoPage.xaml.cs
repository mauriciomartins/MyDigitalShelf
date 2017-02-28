using MyDigitalShelf.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDigitalShelf.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryInfoPage : ContentPage
    {
        private CategoryDetailDirectoryVM categoryDetailDirectoryVM;
        public CategoryInfoPage()
        {
            InitializeComponent();
            this.categoryDetailDirectoryVM = new CategoryDetailDirectoryVM();
            this.BindingContext = categoryDetailDirectoryVM;
            this.SaveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            this.categoryDetailDirectoryVM.SaveDirectory();
            Navigation.PopAsync();
        }
    }
}
