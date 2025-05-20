from pdf2image import convert_from_path
import os

quality = [ 
    "default",  # almost identical to /screen
    "prepress", # high quality, color preserving, 300 dpi imgs
    "printer",  # high quality, 300 dpi images
    "ebook",    # low quality, 150 dpi images
    "screen"    # screen-view-only quality, 72 dpi images
 ]

input_folder = 'in'
output_folder = 'out'

def compress_pdf(input_file, output_filename):
    # Convert PDF to a list of images (each page = one image)
    images = convert_from_path(input_file, dpi=300)
    os.makedirs(output_folder, exist_ok=True)

    for i, image in enumerate(images):
        image.save(f'{output_folder}/{output_filename}_{i + 1}.png', 'PNG')
        print(f"Saved {len(images)} images in '{output_folder}'")


for item in os.listdir(input_folder):
    if item.endswith('.pdf'):
        input_path = os.path.join(input_folder, item)
        filename = os.path.splitext(item)[0]
        compress_pdf(input_path, filename)