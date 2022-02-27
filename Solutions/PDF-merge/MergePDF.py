# Source: https://dev.to/kojo_ben1/how-to-merge-pdf-files-using-the-pypdf2-module-in-python-3lhl

# first do: pip install PyPDF2
import PyPDF2

mergeFile = PyPDF2.PdfFileMerger()
mergeFile.append(PyPDF2.PdfFileReader('trans.pdf', 'rb'))
mergeFile.append(PyPDF2.PdfFileReader('trans2.pdf', 'rb'))
mergeFile.write("NewMergedFile.pdf")