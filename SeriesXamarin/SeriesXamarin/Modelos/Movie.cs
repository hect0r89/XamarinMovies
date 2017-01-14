using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesXamarin.Modelos
{
    public class Movie
    {
        public int id {
            get; set;
        }

        public float rating
        {
            get; set;
        }

        public string title {
            get; set;
        }
        public string summary
        {
            get; set;
        }

        public string language
        {
            get; set;
        }
        public string title_long
        {
            get; set;
        }
        
        public List<String> genres
        {
            get; set;
        }
        public String small_cover_image
        {
            get; set;
        }

        public String medium_cover_image
        {
            get; set;
        }



        public string generosString
        {
            get
            {
                String gener = "";
                foreach (String genero in genres)
                {
                    gener += genero+", ";
                }
                return gener = gener != "" ? gener.Substring(0, gener.Length-2) : "Genre not availabe";
            }
        }

    }


}
