using System;
using System.Linq;

namespace Com.Coderbyte.Solutions.Solutions.Codility
{
    internal class FileRenamer
    {
        public static void Test()
        {
            var input = "photo.jpg, Warsaw, 2013-09-05 14:08:15\r\njohn.png, London, 2015-06-20 15:13:22\r\nmyFriends.png, Warsaw, 2013-09-05 14:07:13\r\nEiffel.jpg, Paris, 2015-07-23 08:03:02\r\npisatower.jpg, Paris, 2015-07-22 23:59:59\r\nBOB.jpg, London, 2015-08-05 00:02:03\r\nnotredame.png, Paris, 2015-09-01 12:00:00\r\nme.jpg, Warsaw, 2013-09-06 15:40:22\r\na.png, Warsaw, 2016-02-13 13:33:50\r\nb.jpg, Warsaw, 2016-01-02 15:12:22\r\nc.jpg, Warsaw, 2016-01-02 14:34:30\r\nd.jpg, Warsaw, 2016-01-02 15:15:01\r\ne.png, Warsaw, 2016-01-02 09:49:09\r\nf.png, Warsaw, 2016-01-02 10:55:32\r\ng.jpg, Warsaw, 2016-02-29 22:13:11";
            var output = solution(input);
            Console.WriteLine(output);
        }

        private static string solution(string S)
        {
            var photos = PhotosHelper.GetPhotos(S);
            photos = PhotosHelper.UpdateNames(photos);
            photos = photos.OrderBy(x => x.ViewOrder).ToList();
            var photoFileNames = photos.Select(x => x.NewFileName).ToList();
            var photosString = string.Join("\r\n", photoFileNames);
            return photosString;
        }
    }



}
