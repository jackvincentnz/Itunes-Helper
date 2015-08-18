using iTunesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesHelper.Helpers
{
    public class PlaylistHelper
    {
        public iTunesApp itunes;

        public PlaylistHelper(iTunesApp itunes)
        {
            this.itunes = itunes;
        }

        public IITUserPlaylist Find(string name, ITUserPlaylistSpecialKind kind, IITUserPlaylist parent)
        {
            foreach (IITPlaylist playlist in itunes.LibrarySource.Playlists)
            {
                if (playlist.Name == name && playlist.Kind == ITPlaylistKind.ITPlaylistKindUser)
                {
                    IITUserPlaylist userPlaylist = (IITUserPlaylist)playlist;
                    if (userPlaylist.SpecialKind == kind)
                    {
                        var foundParent = userPlaylist.get_Parent();
                        if ((foundParent == null && parent == null) || (foundParent != null && foundParent.Name == parent.Name))
                        {
                            return userPlaylist;
                        }
                    }
                }
            }
            return null;
        }
    }
}
