print("""
Άσκηση 20.2 Ανάκτηση τηλεφώνων από ιστοσελίδα τμήματος
""")


import re
import os
import os.path
import urllib.request
import urllib.error

tonoi = ('αά', 'εέ', 'ηή', 'ιί', 'οό', 'υύ', 'ωώ')


os.chdir('D:\Bill\Work\Tutorials\Video\Mathesis\Εισαγωγή στην Python 3.6 (04-17)\Ασκήσεις')

#Εδώ ορίζω ότι στο http request μπαινω μέσω του firefox
my_UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:54.0) Gecko/20100101 Firefox/54.0'
url_avour = 'http://www.ece.upatras.gr/gr/personnel/faculty.html?id=288'
pattern = 'faculty.html?id='
prof_url = 'gr/personnel/faculty.html?id='
url = 'http://www.ece.upatras.gr/gr/personnel/faculty.html'
html = ''

def search_faculty_tel(url):
    try:
        headers = {}
        headers['User-Agent'] = my_UA
        req = urllib.request.Request(url, headers = headers)
        with urllib.request.urlopen(req) as response:
            char_set = response.headers.get_content_charset()
            html = response.read().decode(char_set)
    except urllib.error.HTTPError as e:
        print(e.code)
        print('Σφάλμα HTTP')
        return False
    except urllib.error.URLError as e:
        print('Αποτυχία συνδεσης στον server')
        print('Αιτία: ', e.reason)
        return False
    else:
        title = re.findall(r'<title\b[^>]*>(.*?)</title>', html.replace('\n',''))
        if "Τομέας " in title: return False
        else:
            td = re.findall(r'<td\b[^>]*>(.*?)</td>', html.replace('\n',''))
            tel = ''
            for d in td:
                if 'τηλ' in d:
                    tel = td[td.index(d)+1]
                    tel = re.findall(r'[ 0-9+-/]*', tel)
                    for t in tel:
                        if len(t) > 5 :
                            tel_number = t
                            return (title[0], tel_number)
            return False



if not os.path.isfile('facully_web_page.txt'):
    try:
        headers = {}
        headers['User-Agent'] = my_UA
        req = urllib.request.Request(url, headers = headers)
        with urllib.request.urlopen(req) as response:
            char_set = response.headers.get_content_charset()
            html = response.read().decode(char_set)
    except urllib.error.HTTPError as e:
        print(e.code)
        print('Σφάλμα HTTP')
    except urllib.error.URLError as e:
        if hasattr(e, 'reason'):
            print('Αποτυχία συνδεσης στον server')
            print('Αιτία: ', e.reason)
    else:
        with open('facully_web_page.txt', 'w', encoding = 'utf-8') as f_faculty:
            f_faculty.write(html)
else:
    html = open('facully_web_page.txt', 'r', encoding = 'utf-8').read()
faculty_url = []
if html:
    html = html.replace('\n', '')
    anchors = re.findall(r'<a href="(.*?)"', html)
    for a in anchors:
        if pattern in a:
            faculty_url.append('http://www.ece.upatras.gr'+a)
    for a in faculty_url:
        tel = search_faculty_tel(a)
        if tel: print(tel)
