// <copyright file="MainPage.xaml.cs" company="shrpr.io">
// Copyright (c) 2015 All Rights Reserved
// <author>#R (GitHub: shrpr | Twitter: _shrpr | Stackoverflow: shrpr [4935710])</author>
// </copyright>

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using StyleMe.Resources;
using System.Net.Http;
using Newtonsoft.Json.Linq;

using StyleMe.Zalando;
using System.Threading;

namespace StyleMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Give time for the splash screen to appear
            Thread.Sleep(3000);
        }

        private async void ApplicationBarSummerButton_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonString;

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=SUMMER&agegroup=ADULT&fullText=t-shirt"));
                UpdateImage(jsonString, BodyWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=SUMMER&agegroup=ADULT&fullText=shorts"));
                UpdateImage(jsonString, LegWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=SUMMER&agegroup=ADULT&fullText=hat"));
                UpdateImage(jsonString, HeadWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=SUMMER&agegroup=ADULT&fullText=accessory"));
                UpdateImage(jsonString, Accessory);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=SUMMER&agegroup=ADULT&fullText=shoes"));
                UpdateImage(jsonString, FootWear);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void ApplicationBarWinterButton_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonString;

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=WINTER&agegroup=ADULT&fullText=shirt"));
                UpdateImage(jsonString, BodyWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=WINTER&agegroup=ADULT&fullText=trousers"));
                UpdateImage(jsonString, LegWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=WINTER&agegroup=ADULT&fullText=hat"));
                UpdateImage(jsonString, HeadWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=WINTER&agegroup=ADULT&fullText=accessory"));
                UpdateImage(jsonString, Accessory);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&season=WINTER&agegroup=ADULT&fullText=shoes"));
                UpdateImage(jsonString, FootWear);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void ApplicationBarRandomButton_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonString;

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&agegroup=ADULT&fullText=shirt"));
                UpdateImage(jsonString, BodyWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&agegroup=ADULT&fullText=trousers%20shorts"));
                UpdateImage(jsonString, LegWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&agegroup=ADULT&fullText=hat"));
                UpdateImage(jsonString, HeadWear);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&agegroup=ADULT&fullText=accessory"));
                UpdateImage(jsonString, Accessory);

                jsonString = await RetrieveClothes(new Uri("https://api.zalando.com/articles?gender=MALE&agegroup=ADULT&fullText=shoes"));
                UpdateImage(jsonString, FootWear);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void UpdateImage(string JsonString, System.Windows.Controls.Image TargetImage) 
        {
            try
            {
                // Retrieve JSON Object
                List<Zalando.Content> clothes = JObject.Parse(JsonString).SelectToken("content").ToObject<List<Zalando.Content>>();
                clothes.LastOrDefault();

                // Select a random item from Zalando articles
                Random rnd = new Random();
                int randomItem = 0, imageCount = 0;
                List<Zalando.Image> images = new List<Zalando.Image>();

                // Look for an item with the appropriate image (No models)
                do
                {
                    randomItem = rnd.Next(clothes.Count());
                    images = clothes.ElementAt(randomItem).media.images.Where(p => p.type.StartsWith("NON_MODEL")).ToList();
                    imageCount = images.Count();

                } while (imageCount == 0);

                // Set image
                Zalando.Image clothing = images.FirstOrDefault();
                byte[] clothingByteArray = await RetrievePhoto(new Uri(clothing.mediumHdUrl));

                TargetImage.Source = await ConvertByteArrayToBitmap(clothingByteArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task<string> RetrieveClothes(Uri uri) 
        {
            using (var client = new HttpClient())
            {
                // Connect to the server
                HttpResponseMessage serverResponse = await client.GetAsync(uri);
                serverResponse.EnsureSuccessStatusCode();
                string responseBody = await serverResponse.Content.ReadAsStringAsync();

                // Return List of Clothes from Zalando (JSON string)
                return responseBody;
            }
        }


        public static async Task<byte[]> RetrievePhoto(Uri uri)
        {
            using (var client = new HttpClient())
            {
                // Connect to the server
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                var responseMessage = await client.SendAsync((requestMessage));

                // Get Zalando Item image as a byte array
                byte[] responseData = await responseMessage.Content.ReadAsByteArrayAsync();
                return responseData;
            }
        }


        private async Task<BitmapImage> ConvertByteArrayToBitmap(byte[] ByteArray)
        {
            // Set as async as this conversion can be resource-intensive; Ignore compiler warning
            using (var ms = new MemoryStream(ByteArray))
            {
                var image = new BitmapImage();
                image.SetSource(ms);
                return image;
            }
        }

    }
}