import os
import img2pdf
from PIL import Image

def convertToPdf(file):
    image = Image.open(file)
    pdf_bytes = img2pdf.convert(image.filename)
    image.close()

    pdf_filename = f'{os.path.splitext(file)[0]}.pdf'
    file = open(pdf_filename, "wb")
    file.write(pdf_bytes)
    file.close()

allFiles = [f for f in os.listdir('.') if os.path.isfile(f)]
for file in allFiles:
    if file.endswith('.pdf'):
        continue
    if file.endswith('.py'):
        continue
    convertToPdf(file)