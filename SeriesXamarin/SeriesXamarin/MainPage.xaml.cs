using Newtonsoft.Json;
using SeriesXamarin.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SeriesXamarin
{
    public partial class MainPage : ContentPage
    {
        private int currentPage = 1;
        private Double lastPage = 1;
        String query = "";
        HttpClient client;

        public MainPage()
        {
            InitializeComponent();

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 512000;

            loadShows(currentPage, query);



        }


        public async Task<Request> getShows(int page, String query)
        {
            var uri = new Uri(string.Format("https://yts.ag/api/v2/list_movies.json?page={0}", page));
            if (query != "")
            {

                uri = new Uri(string.Format("https://yts.ag/api/v2/list_movies.json?query_term={0}&page={1}", query, page));
            }


            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Request>(content);
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Info", "Movies not found.", "OK");
            }
            return null;

        }
        public async void loadShows(int page, String query)
        {
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            MovieList.IsVisible = false;
            var list = MovieList;
            Request listShows = await getShows(page, query);
            if (listShows != null)
            {
                if (listShows.data.movie_count > 0)
                {
                    lastPage = Math.Round(listShows.data.movie_count / 20.0);
                    lastButton.Text = lastPage.ToString();
                    list.ItemsSource = listShows.data.movies;
                    activityIndicator.IsVisible = false;
                    activityIndicator.IsRunning = false;
                    MovieList.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Info", "Movies not found.", "OK");
                    activityIndicator.IsVisible = false;
                    activityIndicator.IsRunning = false;
                }

            }
            else
            {
                await DisplayAlert("Error", "Data loaded error.", "OK");
                activityIndicator.IsVisible = false;
                activityIndicator.IsRunning = false;
            }

        }

        public void searchMovie(object sender, EventArgs args)
        {
            currentPage = 1;
            query = Regex.Replace(Search.Text, @"\s+", "+");
            loadShows(currentPage, query);
        }

        public void nextPage(object sender, EventArgs args)
        {


            if (currentPage + 1 < lastPage)
            {
                currentPage++;
                loadShows(currentPage, query);
            }
            else
            {
                DisplayAlert("Info", "Last page.", "OK");
            }

        }

        public void previousPage(object sender, EventArgs args)
        {
            if (currentPage > 1)
            {
                currentPage--;
                loadShows(currentPage, query);
            }
            else
            {
                DisplayAlert("Info", "First page.", "OK");
            }
        }

        public void goFirstPage(object sender, EventArgs args)
        {
            currentPage = 1;
            loadShows(currentPage, query);

        }

        public void goLastPage(object sender, EventArgs args)
        {
            currentPage = Convert.ToInt32(lastPage);
            loadShows(currentPage, query);
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            // Deselect row
            MovieList.SelectedItem = null;
            Movie d = (Movie)e.SelectedItem;
            await Navigation.PushAsync(new DetailPage(d));
        }




    }

}
