using SeriesXamarin.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SeriesXamarin
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(Movie detail)
        {
            InitializeComponent();


            Movie c = detail;
            title.Text = c.title_long;
            image.Source = c.medium_cover_image;
            summary.Text = c.summary;
            genre.Text = "Genre: "+c.generosString;
            rating.Text = c.rating.ToString();
            language.Text ="Language: "+ c.language;
            if (c.rating >= 7)
            {
                rating.BackgroundColor = Color.Green;
            }else if(c.rating<7 && c.rating >= 5)
            {
                rating.BackgroundColor = Color.FromHex("999900");
            }

        }
    }
}
