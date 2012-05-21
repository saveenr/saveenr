from visual import *
import math


scene2 = display(title='Example',
     width=600, height=600,
     center=(0,0,0), background=(0,.1,.5))
 

scene2.autoscale=0

ball = sphere (pos=(0,4,-2), radius=1, color=color.white)
ball.velocity = vector(0,-1,0)

ball2 = sphere (pos=(4,4,-2), radius=.2, color=color.white)
ball2.velocity = vector(0,-1,0)


dt = 0.01

theta = 0

while 1:

    rate (200)

    ball.pos.x = 2 * math.cos( theta )
    ball.pos.y = 2 * math.sin( theta )

    ball2.pos.x = ball.pos.x + 2 * math.cos( theta )
    ball2.pos.y = ball.pos.y + 2 * math.sin( theta )
    

    
    theta += .01


