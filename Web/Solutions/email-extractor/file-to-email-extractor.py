import mailbox
import re
import os

'''
Το παρακάτω κάνει το εξής:
1. Ανοίγει το αρχείο filename.mbox το οποίο βρίσκεται στο Root
2. Το χειρίζεται λες και είναι απλά ένα txt
3. Βρίσκει τα emails μέσα στο content με RegEx
4. Τα αποθηκεύει στην λίστα emails
5. Εκτυπώνει τα μοναδικά emails (λόγω το set(email))
'''

# in the list bockedList you can add all the exceptions you want
blockedList = ['activehosted.com', '@xxx', 'amazonses.com' , 'indeed', 'indeedemail' , 'papaki.gr' , 'gaia.bounces.google.com' , 'notifications.google.com', 'reply', 'hubspot.com']
blockedListRegExPattern = re.compile('|'.join(r'{}'.format(word) for word in blockedList))

def enterFilename():
    filenameInput = input('Enter filename with emails (eg. emails.vbox): ')
    while os.path.isfile(filenameInput) == False:
        filenameInput = input('Wrong! Enter a valid filename with emails (eg. emails.vbox): ')
    return filenameInput

def readFile(filename):
        f = open(filename, 'r', encoding='utf-8')
        b = f.read()
        f.close()
        return(b)

def getRawEmails(context):
        raw_emails = re.findall(r'[\w\.-]+@[\w\.-]+', context)
        emails = raw_emails
        emails = list(dict.fromkeys(emails))
        emails.sort()
        return emails

def cleanRawEmailList(rawEmailList):
        clearList = []
        for i in rawEmailList:
                if blockedListRegExPattern.search(i) == None:
                        clearList.append(i)
        return clearList

def export(emailList, filename):
        extension = os.path.splitext(filename)[1]
        exportingFilename = filename.split(extension)[0]
        exportingFilename = '{}.emails.txt'.format(exportingFilename)
        if os.path.isfile(exportingFilename) == True:
                print(exportingFilename, ': file exist... nothing happen')
        else:
                context = ''
                for i in emailList:
                        context = context + "\n" + i
                thisFile = open(exportingFilename, 'w', encoding='utf-8')
                thisFile.write(context)
                thisFile.close()

def start():
        filename = enterFilename()
        context = readFile(filename)
        rawEmailList = getRawEmails(context)
        emailList = cleanRawEmailList(rawEmailList)
        export(emailList, filename)

for i in range(10):
        start()
