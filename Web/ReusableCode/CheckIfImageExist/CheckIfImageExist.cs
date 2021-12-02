        private string GetRightImgUrl(string imgPathName)
        {
            if (string.IsNullOrEmpty(imgPathName)) return null;
            var url = $"https://www.example.com/source1/{imgPathName}";
            var fileExist = ExistFile(url);
            if (fileExist) return url;
            url = $"https://www.example.com/source2/{imgPathName}";
            fileExist = ExistFile(url);
            if (fileExist) return url;
            return null;
        }

        private bool ExistFile(string url)
        {
            var request = WebRequest.Create(url);
            try { request.GetResponse(); }

            //If exception thrown then couldn't get response from address
            catch { return false; }
            return true;
        }