import unittest
from main import get_youtube_id

class TestGetYoutubeId(unittest.TestCase):

    def test_valid_url(self):
        url = 'http://youtu.be/SA2iWivDJiE'
        self.assertEqual(get_youtube_id(url), 'SA2iWivDJiE')

    def test_invalid_url(self):
        url = 'invalid-url'
        self.assertEqual(get_youtube_id(url), '')

    def test_different_types_of_urls(self):
            #Examples
        url1='http://youtu.be/SA2iWivDJiE'
        url2='http://www.youtube.com/watch?v=_oPAwA_Udwc&feature=feedu'
        url3='http://www.youtube.com/embed/SA2iWivDJiE'
        url4='http://www.youtube.com/v/SA2iWivDJiE?version=3&amp;hl=en_US'
        url5='https://www.youtube.com/watch?v=rTHlyTphWP0&index=6&list=PLjeDyYvG6-40qawYNR4juzvSOg-ezZ2a6'
        url6='youtube.com/watch?v=_lOT2p_FCvA'
        url7='youtu.be/watch?v=_lOT2p_FCvA'
        url8='https://www.youtube.com/watch?time_continue=9&v=n0g-Y0oo5Qs&feature=emb_logo'
        urls=[url1,url2,url3,url4,url5,url6,url7,url8]

        for url in urls:
            id = get_youtube_id(url)
            self.assertEqual(len(id), 11)


if __name__ == '__main__':
    unittest.main()