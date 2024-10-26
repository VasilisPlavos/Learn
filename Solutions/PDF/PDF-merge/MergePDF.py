# Source: https://dev.to/kojo_ben1/how-to-merge-pdf-files-using-the-pypdf2-module-in-python-3lhl

# first do: pip install PyPDF2
import PyPDF2
import os

pdfMerger = PyPDF2.PdfMerger()
pdfFiles = [f for f in os.listdir('.') if os.path.isfile(f) and f.endswith('.pdf')]

for file in pdfFiles:
    fileInput = open(file, "rb")
    pdfMerger.append(file)

# Write to an output PDF document
output = open("document-output.pdf", "wb")
pdfMerger.write(output)