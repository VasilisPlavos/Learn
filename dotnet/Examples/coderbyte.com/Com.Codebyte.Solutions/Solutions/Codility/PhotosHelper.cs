using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Com.Coderbyte.Solutions.Solutions.Codility
{

    public class PhotosHelper
    {
        public static List<PhotoDto> GetPhotos(string photosInputString)
        {
            var lines = photosInputString.Split("\r\n");
            lines = photosInputString.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            var counter = 1;

            return lines.Select(line => new PhotoDto()
            {
                ViewOrder = counter++,
                FileExt = line.Split('.')[1].Split(',')[0],
                DateCreated = GetDate(line),
                Location = line.Split(',')[1].Trim()
            })
                .ToList();
        }

        private static DateTime GetDate(string line)
        {
            // 2016-02-13 13:33:50
            var dateString = line.Split(',')[2].Trim();

            var date = Convert.ToDateTime(dateString);
            //var date = DateTime.Parse(dateString);
            //var date = DateTime.Parse(dateString, CultureInfo.CurrentCulture);
            //var date = Convert.ToDateTime(dateString, new DateTimeFormatInfo { ShortDatePattern = "yyyy-MM-dd T'HH':'mm':'ss" });
            //DateTime.TryParse(dateString, out var date);

            return date;
        }

        public static List<PhotoDto> UpdateNames(List<PhotoDto> photos)
        {
            var locations = photos.Select(x => x.Location).Distinct().ToList();
            foreach (var location in locations)
            {
                var photoNumber = "0";
                var locationPhotos = photos.Where(x => x.Location == location).OrderBy(x => x.DateCreated).ToList();

                foreach (var locationPhoto in locationPhotos)
                {
                    photoNumber = GetPhotoNumber(photoNumber, locationPhotos.Count);
                    locationPhoto.NewFileName = $"{location}{photoNumber}.{locationPhoto.FileExt}";
                }
            }

            return photos;
        }

        private static string GetPhotoNumber(string photoNumber, int photosCount)
        {
            photoNumber = (Convert.ToInt32(photoNumber) + 1).ToString();
            photoNumber = photoNumber.PadLeft(photosCount.ToString().Length, '0');
            return photoNumber;
        }
    }

}