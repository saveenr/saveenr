import Image
import PIL_usm


input_file = "\\foo.jpg"
output_file = "\\foo-blurred.jpg"
blur_radius = 6.5
input_img = Image.open( input_file )
output_img = PIL_usm.gblur( input_img, radius = blur_radius)
output_img.save( output_file )
