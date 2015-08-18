using iTunesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesHelper.Models
{
    public class Playlist
    {
        public IITPlaylist ItunesPlaylist { get; set; }
        public Playlist Parent { get; set; }
        public IList<Playlist> Children { get; set; }

        public Playlist()
        {
            Children = new List<Playlist>();
        }
    }
}
