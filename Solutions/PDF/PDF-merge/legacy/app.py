# pip install PyPDF2
from PyPDF2 import PdfMerger

merger = PdfMerger()
pdfFileNames = [
    "1", 
    "2", 
    "3"
    ]

for fileName in pdfFileNames:
    fileName = fileName+".pdf"
    fileInput = open(fileName, "rb")
    merger.append(fileInput) # append input documents to the end of the output document

# Write to an output PDF document
output = open("document-output.pdf", "wb")
merger.write(output)