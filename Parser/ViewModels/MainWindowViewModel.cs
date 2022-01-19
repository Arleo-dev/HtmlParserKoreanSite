using ReactiveUI;
using System.Collections.Generic;
using Parser.Models;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using System.Net.Http;

namespace Parser.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private string _image;
        private HttpParser _parser;
        private IEnumerable<string> _musicItems = new List<string>();
        private IEnumerable<string> _albumItems = new List<string>();

        #region GetSet
        public string Url { get; private set;}
        public string Image
        {
            get => _image;
            private set => this.RaiseAndSetIfChanged(ref _image, value);
        }
        public IEnumerable<string> AlbumItems
        {
            get => _albumItems;
            private set => this.RaiseAndSetIfChanged(ref _albumItems, value);
        }
        public IEnumerable<string> MusicItems
        {
            get => _musicItems;
            private set => this.RaiseAndSetIfChanged(ref _musicItems, value);
        }

        #endregion

        public void OnClickCommand()
        {
            _parser = new HttpParser(Url);
            var musicItems = _parser.GetParseMusicsInfo();
            var albumInfo = _parser.GetParseAlbumInfo();
            Image =  _parser.Image;
            MusicItems = musicItems;
            AlbumItems = albumInfo;
        }

    }
}
