import os
import subprocess

quality = [ 
    "default",  # almost identical to /screen
    "prepress", # high quality, color preserving, 300 dpi imgs
    "printer",  # high quality, 300 dpi images
    "ebook",    # low quality, 150 dpi images
    "screen"    # screen-view-only quality, 72 dpi images
 ]

def compress_pdf(input_file, output_file, compression_level=3):
    selected_quality = quality[compression_level]
    subprocess.call(['gs', '-sDEVICE=pdfwrite', '-dCompatibilityLevel=1.4',
                    '-dPDFSETTINGS=/' + selected_quality, '-dNOPAUSE', '-dQUIET', '-dBATCH',
                    '-sOutputFile=' + output_file, input_file])

input_folder = 'in'
output_folder = 'out'

if not os.path.exists(output_folder):
    os.makedirs(output_folder)

for item in os.listdir(input_folder):
    if item.endswith('.pdf'):
        input_path = os.path.join(input_folder, item)
        output_path = os.path.join(output_folder, item)
        compress_pdf(input_path, output_path)