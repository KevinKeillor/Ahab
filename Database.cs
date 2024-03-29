﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Common;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace AhabRestService
{

    class MovieDatabase : Database

    {
        private
        String m_DatabaseSource;
        String m_ThumbsPath;
        // Movie info fields
        String m_IdField = "idMovie";
        String m_TitleField = "c00";
        String m_YearField = "c07";
        String m_GenreField = "c14";
        String m_RuntimeField = "c11";
        String m_TagField = "c03";

        String m_PlotField = "c01";
        String m_HDField = "";
        String m_Rating = "c05";

        // Atrist info fields
        String m_AtristIdField = "idActor";
        String m_ArtistNameField = "strActor";

        public
        void SetDbSource(String databaseSource)
        {
            m_DatabaseSource = databaseSource;
        }

        public
        void SetThumbsPath(String ThumbsPath)
        {
            m_ThumbsPath = ThumbsPath;
        }

        
        public 
        Stream GetMovieThumb(int MovieID)
        {
            try
            {
                String commandString = "SELECT path.strPath || files.strFilename " +
                                        "FROM path, files, movie " +
                                        "WHERE movie.idMovie == '" + MovieID + "' " +
                                        "AND movie.idFile == files.idFile " +
                                        "AND path.idPath == files.idPath";

                SQLiteCommand cmd = new SQLiteCommand(commandString, (SQLiteConnection)m_Connection);
                String filePath = (String)cmd.ExecuteScalar();
                String hashedThumb = HashThumb(filePath);
                String movieThumb =  m_ThumbsPath + hashedThumb[0] + "\\" + hashedThumb + ".tbn";

                System.Drawing.Image originalImg = System.Drawing.Image.FromFile(movieThumb);
                float imgRatio = (float)originalImg.Width / (float)originalImg.Height;
                Image.GetThumbnailImageAbort thumbCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                System.Drawing.Image thumbNailImg = originalImg.GetThumbnailImage((int)(120 * imgRatio), 120, thumbCallback, IntPtr.Zero);
                originalImg.Dispose();
               // thumbNailImg.Save(@".\\Movie\\Thumb\\movieThumb\\" + hashedThumb + ".png", ImageFormat.Png);
                // return stream as Stream;
                var stream = new System.IO.MemoryStream();
                thumbNailImg.Save(stream, ImageFormat.Png);
                thumbNailImg.Dispose();
                stream.Position = 0;
                return stream;
            }
            catch
            {

            }
            return null;
        }


        public
        List<MovieSumary> GetMovieSummaryList()
        {
            try
            {
                Dictionary<Int64, MovieSumary> movieList = new Dictionary<Int64,MovieSumary> { };
                String commandString = "SELECT movie." + m_IdField + ", movie." + m_TitleField + ", movie." + m_YearField +
                    " , movie." + m_GenreField + ", movie." + m_RuntimeField + ", movie." + m_TagField + ", movie." + m_PlotField + " FROM movie ";            

                SQLiteCommand sqlCommand = new SQLiteCommand(commandString, (SQLiteConnection)m_Connection);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    
                    MovieSumary movieInfo = new MovieSumary { };
                    // get the results of each column
                    Int64 currentId = (Int64)reader[m_IdField];
                    movieInfo.m_Id = currentId.ToString();
                    movieInfo.m_Title = (String)reader[m_TitleField];
                    movieInfo.m_Year = (String)reader[m_YearField];
                    movieInfo.m_Genre = (String)reader[m_GenreField];

                    int time = int.Parse((String)reader[m_RuntimeField]);
                    TimeSpan span = System.TimeSpan.FromMinutes(time);
                    int hours = (int)span.TotalHours;
                    int minutes = span.Minutes;

                    String Duration = "";
                    if (hours > 0)               
                        Duration += hours + "h ";

                    Duration += minutes + "min";
                    movieInfo.m_Runtime = Duration;
                    movieInfo.m_Tagline = (String)reader[m_TagField];
                    movieInfo.m_CastList = new List<Artist> { };
                    movieInfo.m_Plot = (String)reader[m_PlotField];
                    
 
                    // Add the completed MovieSummary to the list
                    movieList.Add(currentId, movieInfo);                                 
                }


                commandString = "SELECT movie." + m_IdField + " ,actorlinkmovie.idActor ,actorlinkmovie.iOrder  ,actorlinkmovie.strRole "+
                    "FROM movie , actorlinkmovie " +
                    "WHERE movie.idMovie = actorlinkmovie.idMovie " +
                    "ORDER BY actorlinkmovie.iOrder ";

                sqlCommand = new SQLiteCommand(commandString, (SQLiteConnection)m_Connection);
                reader = sqlCommand.ExecuteReader();


                Int64 Id = (Int64)reader[m_IdField];                
                Int64 lastId = 0;
   
                while (reader.Read())
                {
                    lastId = (Int64)reader[m_IdField];
                    Int64 ActorId = (Int64)reader["idActor"];
                    String name = (String)reader["strRole"];
                    movieList[lastId].m_CastList.Add(new Artist((int)ActorId, name));                    
                }

                
                //movieList.
                return new List<MovieSumary>(movieList.Values);

            }
            catch
            {

            }
            return null;
        }

        private bool ThumbnailCallback()
        {
            return false;
        }


        public
        List<Artist> GetMovieArtistList()
        {
            try
            {
                List<Artist> artistList = new List<Artist> { };
                String commandString = "SELECT " + m_AtristIdField + ", " + m_ArtistNameField + " FROM actors";

                SQLiteCommand sqlCommand = new SQLiteCommand(commandString, (SQLiteConnection)m_Connection);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Artist artist = new Artist { };
                    // get the results of each column
                    artist.m_Id = ((Int64)reader[m_AtristIdField]).ToString();
                    artist.m_Name = (String)reader[m_ArtistNameField];

                    artistList.Add(artist);
                }
                //artistList.
                return artistList;

            }
            catch
            {

            }
            return null;
        }

        public bool Open()
        {
            try
            {
                m_Connection = new SQLiteConnection("Data Source=" + m_DatabaseSource);
                m_Connection.Open();
                return true;
            }
            catch
            {

            }
            return false;
        }
    }




    class SQLDatabase : Database
    {


        private String m_Instance;
        private String m_Database;
        private String m_UserId;
        private String m_Password;

        public
        void SetConnection(String instance, String database, String userId, String password)
        {
            m_Instance = instance;
            m_Database = database;
            m_UserId = userId;
            m_Password = password;
        }

        public
        bool Open()
        {
            try
            {
                SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();

                bldr.DataSource = ".\\SQLEXPRESS"; //Put your server or server\instance name here. Likely YourComputerName\SQLExpress

                bldr.InitialCatalog = "home"; //The database on the server that you want to connect to.

                bldr.UserID = "client"; //The user id

                bldr.Password = "password"; //The pwd for said user account
                m_Connection = new SqlConnection(bldr.ConnectionString);
                m_Connection.Open();
            }
            catch
            {

            }
            return false;
        }
    }

    partial class Database
    {
        public
        DbConnection m_Connection;

        public bool Close()
        {
            try
            {
                m_Connection.Close();
            }
            catch
            {

            }
            return false;
        }

        public String WrapXMLString(String XML)
        {
            return "<w>" + XML + "</w>";
        }

        public string HashThumb(string input)
        {
            char[] chars = input.ToCharArray();
            for (int index = 0; index < chars.Length; index++)
            {
                if (chars[index] <= 127)
                {
                    chars[index] = System.Char.ToLowerInvariant(chars[index]);
                }
            }
            input = new string(chars);
            uint m_crc = 0xffffffff;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            foreach (byte myByte in bytes)
            {
                m_crc ^= ((uint)(myByte) << 24);
                for (int i = 0; i < 8; i++)
                {
                    if ((System.Convert.ToUInt32(m_crc) & 0x80000000) == 0x80000000)
                    {
                        m_crc = (m_crc << 1) ^ 0x04C11DB7;
                    }
                    else
                    {
                        m_crc <<= 1;
                    }
                }
            }
            return String.Format("{0:x8}", m_crc);
        }
    }
}