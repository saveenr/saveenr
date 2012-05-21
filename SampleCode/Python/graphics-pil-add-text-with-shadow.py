import Image
import PIL_usm
import math
import ImageGrab
import sys



import Image
import ImageDraw
import ImageFont
import ImageChops


def create_text_shadow( text, labelfont, blur_radius ) :

    # first figure out how much space is required for the text
    dummy_img= Image.new( 'L', (16,16) )
    dummy_draw= ImageDraw.Draw(dummy_img)
    text_extent = dummy_draw.textsize( text, font=labelfont )

    # Calculate the size needed for the image
    deltax = int(math.ceil( blur_radius ) )
    deltay = deltax
    
    img_size = ( text_extent[0]+(2*deltax), text_extent[1]+(2*deltay))

    # because the shadow exists we can't just draw at (0,0)
    # instead, adjust to figure out the correct coordinate
    
    text_pos = (deltax,deltay)
    

    # Create a image to store the shadow alpha
    alpha_band = Image.new( 'L', img_size)
    draw = ImageDraw.Draw( alpha_band )
    draw.text( text_pos, text , font=labelfont, fill="#ffffff")
    alpha_band = PIL_usm.gblur( alpha_band, radius=blur_radius )

    # Now convert to regular RGBA image that contains a shadow
    output_img = Image.new( 'RGBA', img_size)
    output_img.putalpha( alpha_band )
    
    return output_img,text_pos 


def add_text_with_shadow( background_img, text, labelfont, textpos, blur_radius, shadow_delta, intensification_factor = 1) :

    shadow_img,text_displacement= create_text_shadow( text, labelfont, blur_radius )

    background_img = background_img.convert( 'RGBA' )

    shadow_pos = ( textpos[0] - text_displacement[0] + shadow_delta[0], textpos[1] - text_displacement[1] + + shadow_delta[1])     

    
    for i in xrange(intensification_factor) :
        background_img.paste( "#000000" , shadow_pos , shadow_img )
   
    draw = ImageDraw.Draw(background_img)
    draw.text( textpos, text , font=labelfont, fill="#ffffff")

    return background_img

def main() :

    in_fname = "\\foo.jpg"
    out_fname = "\\foo-with-text.png" # save as png so that the modfications are clearly seen

    text = "Text with shadow"
    original_pos = (56,31)
    font_size = 12
    ttf_filename = r'c:\windows\fonts\arial.ttf'
    shadow_displacement = (1,1)
    blur_radius = 2.0
    


    background_image = ImageGrab.grab( (0,0,500,500) )
    arial_font = ImageFont.truetype( ttf_filename, font_size)
    output_image = add_text_with_shadow( background_image , text, arial_font, original_pos , blur_radius, shadow_displacement) 
    output_image.save( "\\output.png" )


if (__name__=='__main__') :
    main()