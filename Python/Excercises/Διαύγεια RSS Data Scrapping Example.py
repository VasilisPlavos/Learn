print("""
21.2 Αναζήτηση αναρτήσεων στη «διαύγεια»
http://diavgeia.gov.gr
H γνωστή ιστοσελίδα ανάρτησης πράξεων της δημόσιας διοίκησης
στο διαδίκτυο παρέχει υπηρεσία rss feed (βλέπε: https://diavgeia.gov.gr/blog/?p=116)
Να κατασκευάσετε εφαρμογή που να επιτρέπει
στον χρήστη να αναζητήσει τις τελευταίες πράξεις κάποιου δημόσιου φορέα.
""")



# 21.3 Tελική εργασία: Ανάκτηση δεδομένων από τη diavgeia.gov.gr
# Πρότυπο λύσης

import re
import urllib.request
import urllib.error
import os
import os.path
tonoi = ('αά', 'εέ', 'ηή', 'ιί', 'οό', 'υύ', 'ωώ')
os.chdir('D:\Bill\Work\Tutorials\Video\Mathesis\Εισαγωγή στην Python 3.6 (04-17)\Ασκήσεις\diavgeia')
my_UA = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:54.0) Gecko/20100101 Firefox/54.0'
url = ''
arxes = {}
temp = ''

def rss_feed(url): #3 μονάδες *
    url += r"/rss"
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
        html = html.replace('\n', '')
        return process_feed(html)
    
    '''
    Άνοιγμα του rss feed,
    :param url: η διεύθυνση του rss feed.
    Αυτή η συνάρτηση δημιουργεί ένα αρχείο
    με τα περιεχόμενα του rss_feed με όνομα
    την διεύθυνση του rss feed.
    Καλεί την συνάρτηση process_feed
    η οποία επιλέγει και τυπώνει περιεχόμενο
    Προσπαθήστε να κάνετε try/except τα exceptions
    HTTPError και URLError.
    '''
    #σύμφωνα με την ανακοίνωση της διαύγειας τα rss feeds είναι στο ίδιο url/rss



def process_date(date): #2 μονάδες
    date = date.replace('/01/', ' Ιαν ')
    date = date.replace('/02/', ' Φεβ ')
    date = date.replace('/03/', ' Μαρ ')
    date = date.replace('/04/', ' Απρ ')
    date = date.replace('/05/', ' Μάι ')
    date = date.replace('/06/', ' Ιουν ')
    date = date.replace('/07/', ' Ιουλ ')
    date = date.replace('/08/', ' Αυγ ')
    date = date.replace('/09/', ' Σεπτ ')
    date = date.replace('/10/', ' Οκτ ')
    date = date.replace('/11/', ' Νοε ')
    date = date.replace('/12/', ' Δεκ ')
    return date
    
    
    
    '''
    η συνάρτηση διαμορφώνει την ελληνική ημερομηνία του rss feed:
    Στο rss αρχείο η ημερομηνία είναι της μορφής: Wed, 14 Jun 2017 17:21:16 GMT
    Θα πρέπει να διαμορφώνεται σε ελληνική ημερομηνία, πχ: Τετ, 14 Ιουν 2017
    :param date:
    :return: η ελληνική ημερομηνία
    '''
    pass

def process_feed(filename): #3 μονάδες *
    tag = 'item'
    items = re.findall(r'<' + tag + r'\b[^>]*>(.*?)</' + tag + r'>', filename, re.I)    
    for i in items:
        tag = 'title'
        title = re.findall(r'<' + tag + r'\b[^>]*>(.*?)</' + tag + r'>', i, re.I)        
        tag = 'description'
        desc = re.findall(r'<' + tag + r'\b[^>]*>(.*?)</' + tag + r'>', i, re.I)
        time = desc[0].split('Ημ/νια:&lt;/strong&gt; ')[1]
        time = time.split('&lt;br /&gt;&lt;strong&gt;Φορέας:')[0]
        time = time.split(' ')[0]
        time = process_date(time)
        print(time, title[0])
    return items
    '''
    συνάρτηση που ανοίγει το αρχείο με το rss feed και 
    τυπώνει την ημερομηνία και τους τίτλους των αναρτήσεων που περιέχει.
    Xρησιμοποιήστε regular expressions 
    '''


def search_arxes(arxh): #2 μονάδες
    found = []
    pattern = '.*'+arxh+'.*'
    for i in arxes.keys():
        w = re.findall(pattern, i, re.I)
        if len(w)>0:
            found.append(w[0])
    return found
    '''
    Αναζήτηση ονόματος Αρχής που ταιριάζει στα κριτήρια του χρήστη
    '''

def load_arxes(): #2 μονάδες
    filename = "500_arxes.csv"
    try:
        with open(filename, 'r', encoding = 'utf-8') as f:
            text = f.read()
            temp = text
        for line in text.split('\n'):
            try:
                arxes[line.split(';')[0]] = line.split(';')[1]
                print(line)
            except IndexError as e:
                e
    except IOError as e:
        print(e)
    
    '''
    φορτώνει τις αρχές στο λεξικό arxes{}
    '''
    
######### main ###############
'''
το κυρίως πρόγραμμα διαχειρίζεται την αλληλεπίδραση με τον χρήστη
'''
load_arxes()

##tha svistei meta
line = 'ΥΠΟΥΡΓΕΙΟ ΑΓΡΟΤΙΚΗΣ ΑΝΑΠΤΥΞΗΣ ΚΑΙ ΤΡΟΦΙΜΩΝ;https://diavgeia.gov.gr/f/YAAT'


while True :
    arxh = input(50*"^"+"\nΟΝΟΜΑ ΑΡΧΗΣ:(τουλάχιστον 3 χαρακτήρες), ? για λίστα:")
    if arxh == '':
        break
    elif arxh == "?": # παρουσιάζει τα ονόματα των αρχών
        for k,v in arxes.items():
            print (k,v)
    elif len(arxh) >= 3 :
        # αναζητάει όνομα αρχής που ταιριάζει στα κριτήρια του χρήστη
        result = search_arxes(arxh)
        for r in result:
            print (result.index(r)+1, r, arxes[r])
        while result:
            epilogh = input("ΕΠΙΛΟΓΗ....")
            if epilogh == "": break
            elif epilogh.isdigit() and 0<int(epilogh)<len(result)+1:
                epilogh = int(epilogh)
                url = arxes[result[epilogh-1]]
                print(url)
                # καλεί τη συνάρτηση που φορτώνει το αρχείο rss:
                rss_feed(url)
                temp = rss_feed(url)
                
            else: continue
    else :
        continue
