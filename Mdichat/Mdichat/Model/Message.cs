using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiChat.Model
{
    public class Message : ObservableObject, INotifyPropertyChanged
    {
        string text;

        public string Text
        {
            get => text;
            set => this.text = value;
        }

        DateTime messageDateTime;

        public DateTime MessageDateTime
        {
            get => messageDateTime;
            set => this.messageDateTime = value;
        }

        public string MessageTimeDisplay => MessageDateTime.ToString();

        bool isIncoming;

        public bool IsIncoming
        {
            get => isIncoming;
            set => this.isIncoming = value;
        }

        public bool HasAttachement => !string.IsNullOrEmpty(attachementUrl);

        string attachementUrl;

        public string AttachementUrl
        {
            get => attachementUrl;
            set => this.attachementUrl = value;
        }

        byte[] userImage;
        public byte[] UserImage
        {
            get => userImage ?? new byte[0];
            set
            {
                this.userImage = value;
                OnPropertyChanged("UserImage");
            }
        }
        string userImagePath;
        public string UserImagePath
        {
            get => userImagePath ?? string.Empty;
            set
            {
                this.userImagePath = value;
                OnPropertyChanged("UserImagePath");
            }
        }

        byte[] fileData;
        public byte[] FileData
        {
            get => fileData ?? new byte[0];
            set
            {
                this.fileData = value;
                this.IsFileIsImage = true;
                OnPropertyChanged("FileData");
            }
        }

        private string imageFilePath;
        // <img src="https://placeholdit.co//i/275x150?&text=MDI">

        public string ImageFilePath
        {
            get => imageFilePath ?? "https://placeholdit.co//i/275x150?&text=MDI";
            set
            {
                this.imageFilePath = value;
                this.IsFileIsImage = true;
                OnPropertyChanged("ImageFilePath");
            }
        }

        public int Id { get; set; }

        private bool _isFileImage;
        public bool IsFileIsImage
        {
            get => _isFileImage;
            set => this._isFileImage = value;
        }

        int senderId;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public int SenderId
        {
            get => senderId;
            set => this.senderId = value;
        }

        // Create the OnPropertyChanged method to raise the event
        protected override void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
