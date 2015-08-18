using iTunesLib;
using System;
using System.IO;
using ItunesHelper.Helpers;
using System.Collections.Generic;

namespace ItunesHelper
{
    class Program
    {
        public static iTunesApp _itunes;
        public static iTunesApp itunes { get { return _itunes ?? (_itunes = new iTunesApp()); } }

        public static PlaylistHelper _playlistHelper;
        public static PlaylistHelper playlistHelper { get { return _playlistHelper ?? (_playlistHelper = new PlaylistHelper(itunes)); } }

        static void Main(string[] args)
        {            
            // Hard coded for testing purposes
            args = new[] { "C:\\Users\\Jack\\Music\\DJ" };

            foreach (string path in args)
            {
                ProcessDirectory(path, null);
            }
        }

        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static void ProcessDirectory(string path, IITUserPlaylist parent)
        {
            Console.WriteLine("Processing directory '{0}'.", path);

            if (Directory.Exists(path))
            {
                var dir = new DirectoryInfo(path);
                
                IITUserPlaylist folder = null;
                var isFolder = DirectoryHelper.ContainsDirectories(path);
                if (isFolder)
                {
                    var fName = dir.Name;
                    // check if playlist folder exists at current location
                    folder = playlistHelper.Find(fName, ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindFolder, parent);
                    if (folder == null)
                    {
                        if (parent != null)
                        {
                            folder = (IITUserPlaylist)parent.CreateFolder(fName);
                        }
                        else
                        {
                            folder = (IITUserPlaylist)itunes.CreateFolder(fName);
                        }
                    }
                }
                
                IITUserPlaylist playlist = null;
                if (DirectoryHelper.ContainsFiles(path))
                {
                    var pName = isFolder ? "## Tracks ##" : dir.Name;
                    var playlistLocation = isFolder ? folder : parent;
                    playlist = playlistHelper.Find(pName, ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindNone, playlistLocation);
                    if (playlist == null)
                    {
                        if (playlistLocation != null)
                        {
                            playlist = (IITUserPlaylist)playlistLocation.CreatePlaylist(pName);
                        }
                        else
                        {
                            playlist = (IITUserPlaylist)itunes.CreatePlaylist(pName);
                        }
                    }

                    var tracks = playlist.Tracks;
                    var trackPaths = new List<string>();
                    foreach (IITTrack track in tracks)
                    {
                        trackPaths.Add(track.)
                    }

                    string[] fileEntries = Directory.GetFiles(path);
                    foreach (var file in fileEntries)
                    {
                        Console.WriteLine("Processing file '{0}'.", file);

                        playlist.AddFile(file);
                    }
                }

                // Recursively process the child folders
                string[] subDirectories = Directory.GetDirectories(path);
                foreach (string subDirectory in subDirectories)
                {
                    ProcessDirectory(subDirectory, folder);
                }
            }
            else
            {
                Console.WriteLine("{0} is not a valid directory.", path);
            }
        }
    }
}

