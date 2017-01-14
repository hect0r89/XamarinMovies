using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesXamarin.Modelos
{
    public class Data
    {
        public int movie_count
        {
            get; set;
        }

        public int page_number
        {
            get; set;
        }

        public List<Movie> movies
        {
            get; set;
        }
    }
}
