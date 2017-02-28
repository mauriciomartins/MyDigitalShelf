using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDigitalShelf.model
{
    class ContentDirectoryVM : ObservableBaseObject
    {
        public ObservableCollection<Content> ContentList { get; set; }
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; OnPropertyChanged(); }
        }

        public Command LoadContentDirectoryCommand
        {
            get; set;
        }

        public ContentDirectoryVM()
        {
            this.ContentList = new ObservableCollection<Content>();
            this.IsBusy = false;
            this.LoadContentDirectoryCommand = new Command(() => LoadDirectory(),()=>!this.IsBusy);
        }

        public async void LoadDirectory()
        {
            if (!IsBusy)
            {
                IsBusy = true;

               // await Task.Delay(3000);
                var loadDirectory = ContentDirectoryService.LoadContentDirectory();

                foreach (var content in loadDirectory.ContentList)
                {
                    this.ContentList.Add(content);
                }

                IsBusy = false;
            }
        }
    }
}
