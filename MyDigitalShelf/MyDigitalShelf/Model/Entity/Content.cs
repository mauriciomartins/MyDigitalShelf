using MyDigitalShelf.Model.Entity;
using MyDigitalShelf.Model.Storage;

namespace MyDigitalShelf.model
{
    class Content : ObservableBaseObject, IKeyObject
    {
        private int position;
        private string value;
        private string valueSound;
        private string description;
        private string descriptionSound;
        public int Position
        {
            get { return this.position; }
            set { this.position = (int) value; OnPropertyChanged(); }
        }
        public string Value {
            get { return this.value; }
            set { this.value = value; OnPropertyChanged(); }
        }
        public string ValueSound
        {
            get { return this.valueSound; }
            set { this.valueSound = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; OnPropertyChanged(); }
        }
        public string DescriptionSound
        {
            get { return this.descriptionSound; }
            set { this.descriptionSound = value; OnPropertyChanged(); }
        }

        public string Key
        {
            get;
            set;
        }

    }
}
