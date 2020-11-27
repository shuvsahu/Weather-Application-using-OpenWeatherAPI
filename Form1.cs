using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        const string APPID = "542ffd081e67f4512b705f89d2a611b2";
        string cityName = "sydney";
        public Form1()
        {
            InitializeComponent();
            getWeather(cityName);
            getForecast(cityName);
        }

        void getWeather(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6,", city, APPID);

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                WeatherInfo.root output = result;

                lbl_cityName.Text = string.Format("{0}", output.name);
                lbl_country.Text = string.Format("{0}", output.sys.country);
                lbl_Temp.Text = string.Format("{0}\u00B0", Convert.ToInt32(output.main.temp));
                lbl_feels.Text = string.Format("Feels like {0}\u00B0C", Convert.ToInt32(output.main.feels_like));
                lbl_pressure.Text = string.Format("Barometer {0}mb", Convert.ToInt32(output.main.pressure));
                lbl_humid.Text = string.Format("Humidity {0}%", Convert.ToInt32(output.main.humidity));
                lbl_wind.Text = string.Format("Wind {0}km/h", (output.wind.speed) * 18 / 5);
                lbl_time.Text = string.Format("Updated as of {0}", UnixTimeStampToDateTime(output.dt).TimeOfDay);
                lbl_date.Text = string.Format("{0}", UnixTimeStampToDateTime(output.dt).ToShortDateString());
                lbl_day.Text = string.Format("{0}", UnixTimeStampToDateTime(output.dt).DayOfWeek);
                lbl_direc.Text = string.Format("{0}", DegreesToCardinalDetailed(output.wind.deg));
                lbl_cond.Text = string.Format("{0}", output.weather[0].main);
                lbl_lon.Text = string.Format("Longitude {0}", output.coord.lon);
                lbl_lat.Text = string.Format("Latitude {0}", output.coord.lat);
            }
        }

        void getForecast(string city)
        {
            int day = 4;
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt={1}&appid={2}", city, day, APPID);
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);

                var Object = JsonConvert.DeserializeObject<WeatherForecast>(json);

                WeatherForecast forecast = Object;

                lbl_mm.Text = string.Format("Max/Min {0}\u00B0/{1}\u00B0C", Convert.ToInt32(forecast.list[0].temp.max), Convert.ToInt32(forecast.list[0].temp.min));
                lbl_des.Text = string.Format("{0}", forecast.list[0].weather[0].description);

                lbl_day_2.Text = string.Format("{0}", UnixTimeStampToDateTime(forecast.list[1].dt).DayOfWeek);
                lbl_cond_2.Text = string.Format("{0}", forecast.list[1].weather[0].main);
                lbl_des_2.Text = string.Format("{0}", forecast.list[1].weather[0].description);
                lbl_temp_2.Text = string.Format("{0}\u00B0/{1}\u00B0C", Convert.ToInt32(forecast.list[1].temp.max), Convert.ToInt32(forecast.list[1].temp.min));
                lbl_wind_2.Text = string.Format("{0}km/h", forecast.list[1].speed);

                lbl_day_3.Text = string.Format("{0}", UnixTimeStampToDateTime(forecast.list[2].dt).DayOfWeek);
                lbl_cond_3.Text = string.Format("{0}", forecast.list[2].weather[0].main);
                lbl_des_3.Text = string.Format("{0}", forecast.list[2].weather[0].description);
                lbl_temp_3.Text = string.Format("{0}\u00B0/{1}\u00B0C", Convert.ToInt32(forecast.list[2].temp.max), Convert.ToInt32(forecast.list[2].temp.min));
                lbl_wind_3.Text = string.Format("{0}km/h", forecast.list[2].speed);

                lbl_day_4.Text = string.Format("{0}", UnixTimeStampToDateTime(forecast.list[3].dt).DayOfWeek);
                lbl_cond_4.Text = string.Format("{0}", forecast.list[3].weather[0].main);
                lbl_des_4.Text = string.Format("{0}", forecast.list[3].weather[0].description);
                lbl_temp_4.Text = string.Format("{0}\u00B0/{1}\u00B0C", Convert.ToInt32(forecast.list[3].temp.max), Convert.ToInt32(forecast.list[3].temp.min));
                lbl_wind_4.Text = string.Format("{0}km/h", forecast.list[3].speed);
            }
        }

        public static string DegreesToCardinalDetailed(double degrees)
        {
            int val = Convert.ToInt32((degrees / 22.5) + .5);

            string[] caridnals = { "N","NNE","NE","ENE","E","ESE", "SE", "SSE","S","SSW","SW","WSW","W","WNW","NW","NNW" };
            return caridnals[(val % 16)];
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void lbl_Temp_Click(object sender, EventArgs e)
        {

        }

        private void lbl_feels_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbl_cond_3_Click(object sender, EventArgs e)
        {

        }
    }

}

