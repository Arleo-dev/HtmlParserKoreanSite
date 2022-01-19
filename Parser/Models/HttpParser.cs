using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Models
{
    class HttpParser
    {
        private const string TitleXpath = "//p[@class = 'title']//a";
        private const string AlbumNameXpath = "//header[@class = 'sectionPadding pgTitle noneLNB']//h1";
        private const string AvatarXpath = "//li[@class='big']//img";
        private const string DateXpath = "//table[@class = 'info']//time";
        private const string GenreXpath = "//th[contains(text(), \"장르\")]//../td";
        private const string RecorLabelXpath = "//th[contains(text(), \"유통사\")]//../td";
        private const string ArtistXpath = "//p[@class = 'artist']//a";

        private string _image;
        private string _url;
        private HtmlWeb _web;
        private HtmlDocument _doc;

        public string Image => _image;
        public HttpParser(string url)
        {
            if (url == null)
            {
                url = "https://music.bugs.co.kr/album/4070248?wl_ref=M_contents_01_04";
            }
            _url = url;
        }

        public IEnumerable<string> GetParseAlbumInfo()
        {
            _web = new HtmlWeb();
            _doc = _web.Load(_url);

            var listAlbumInfo = new List<string>();
            var avatar = _doc.DocumentNode.SelectSingleNode(AvatarXpath).Attributes["src"].Value.Split("?");
            var albumName = _doc.DocumentNode.SelectSingleNode(AlbumNameXpath);
            var date = _doc.DocumentNode.SelectSingleNode(DateXpath);
            var genre = _doc.DocumentNode.SelectSingleNode(GenreXpath);
            var recordLabel = _doc.DocumentNode.SelectSingleNode(RecorLabelXpath);
            _image = avatar[0];
            listAlbumInfo.Add($"Album: {albumName.InnerText}");
            listAlbumInfo.Add($"Date: {date.InnerText}");
            listAlbumInfo.Add($"Genre: {genre.InnerText}");
            listAlbumInfo.Add($"Record: {recordLabel.InnerText}");

            return listAlbumInfo;
        }
        public IEnumerable<string> GetParseMusicsInfo()
        {
            _web = new HtmlWeb();
            _doc = _web.Load(_url);
            var listMusicInfo = new List<string>();
            var title = _doc.DocumentNode.SelectNodes(TitleXpath);
            var artist = _doc.DocumentNode.SelectNodes(ArtistXpath);
            var albumName = _doc.DocumentNode.SelectSingleNode(AlbumNameXpath);
            for (int i = 0; i < title.Count; i++)
            {
                listMusicInfo.Add($"Music: {title[i].InnerText} \nArtist: {artist[i].InnerText} \nAlbum:{albumName.InnerText}");
            }
            return listMusicInfo;
        }
    }
}
