print("""
19.4 Φτιάξτε μια μηχανή αναζήτησης για τα παραμύθια του Ευγένιου Τριβιζά
""")


import re
import os
import os.path
tonoi = ('αά', 'εέ', 'ηή', 'ιί', 'οό', 'υύ', 'ωώ')


os.chdir('D:\Bill\Work\Tutorials\Video\Mathesis\Εισαγωγή στην Python 3.6 (04-17)\Ασκήσεις')
tw = "Trivizas_works.txt"
try:
    with open(tw,'r', encoding = 'utf-8') as f:
        works = f.read()
    for line in works.split('\n'):
        print(line)
except IOError as e:
    print(e)
while True:
    phrase = input("Δώσε λεξη-κλειδί: ")
    if phrase == '': break
    n_phrase = ''
    for c in phrase:
        char = c
        for t in tonoi:
            if c in t: char = '['+t+']'
        n_phrase += char
    print(n_phrase)

    
    pattern = '.*'+n_phrase+'.*'
    w = re.findall(pattern, works, re.I)
    for work in w:
        print(work)
