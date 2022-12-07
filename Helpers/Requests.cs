using MovieApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Helpers
{
    public class Requests
    {
        public static HttpClient Client { get; set; }

        private string ApiUrl { get; set; }

        private readonly string apiKey = "api_key=2d349213201c63ae7bb7bac54f6bc75e";

        public Requests()
        {
            ApiUrl = "https://api.themoviedb.org/3";

            Client = new HttpClient();
        }

        public async Task<List<GenreItemModel>> FetchGenreList(string endpoint)
        {
            string url = $"{ApiUrl}/{endpoint}?{apiKey}";

            try
            {
                GenresModel data;

                HttpResponseMessage response = await Client.GetAsync(url);

                data = await response.Content.ReadAsAsync<GenresModel>();

                if (data?.Genres == null)
                {
                    throw new Exception();
                }

                List<GenreItemModel> genres = data.Genres;

                genres.Insert(0, new GenreItemModel { Id = 0, Name = "Popular" });

                return genres;
            }
            catch
            {
                return null;
            }
        }

        public async Task<MoviesModel<MovieItemModel>> FetchMovies(string endpoint, int page, string queryParmas = "")
        {
            string url = $"{ApiUrl}/{endpoint}?{apiKey}&{queryParmas}&sort_by=popularity.desc&page={page}";

            try
            {
                MoviesModel<MovieItemModel> data;

                HttpResponseMessage response = await Client.GetAsync(url);

                data = await response.Content.ReadAsAsync<MoviesModel<MovieItemModel>>();

                return data?.Results == null || data?.Results.Count < 1 ? throw new Exception() : data;
            }
            catch
            {
                return null;
            }
        }

        public async Task<MovieDetailsModel> FetchMovieDetails(string endpoint, int id)
        {
            string url = $"{ApiUrl}/{endpoint}/{id}?{apiKey}&append_to_response=videos";

            try
            {
                MovieDetailsModel movieDetails;
                ActorsModel<ActorItemModel> actors;

                HttpResponseMessage movieDetailsResponse = await Client.GetAsync(url);
                HttpResponseMessage actorListResponse = await Client.GetAsync($"{ApiUrl}/{endpoint}/{id}/credits?{apiKey}");

                movieDetails = await movieDetailsResponse.Content.ReadAsAsync<MovieDetailsModel>();
                actors = await actorListResponse.Content.ReadAsAsync<ActorsModel<ActorItemModel>>();

                if (movieDetails == null || actors?.Cast.Count() < 1)
                {
                    throw new Exception();
                }

                movieDetails.Actors = actors.Cast;

                return movieDetails;
            }
            catch
            {
                return null;
            }
        }

        public async Task<MovieDetailsModel> FetchRandomMovie(string endpoint, string from, string to, string genre = "")
        {
            string url = $"{ApiUrl}/{endpoint}?{apiKey}&{genre}&primary_release_date.gte={from}&primary_release_date.lte={to}&vote_count.gte=75";

            Random rnd = new Random();

            try
            {
                MoviesModel<MovieItemModel> movieList;
                MovieDetailsModel data;

                HttpResponseMessage response = await Client.GetAsync(url);

                movieList = await response.Content.ReadAsAsync<MoviesModel<MovieItemModel>>();

                int pagesCount = movieList.Total_Pages > 500 ? 500 : movieList.Total_Pages;

                int page = rnd.Next(1, pagesCount);
                int movieIndex = rnd.Next(0, movieList.Results.Count());

                response = await Client.GetAsync($"{url}&page={page}");
                movieList = await response.Content.ReadAsAsync<MoviesModel<MovieItemModel>>();

                data = await FetchMovieDetails("movie", movieList.Results[movieIndex].Id);

                return data ?? throw new Exception();
            }
            catch
            {
                return null;
            }
        }
    }
}
