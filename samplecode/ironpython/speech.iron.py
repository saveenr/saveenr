
import clr
clr.AddReference("System.Speech")

from System.Speech.Synthesis import *

synth = SpeechSynthesizer()

synth.Speak("Slated for release in the second half of 2007, Houdini 9 is going to be exciting for both existing users and for artists new to Houdini. In addition to a new fluid solver that is fully integrated into Houdini's dynamics (DOPs) environment, there will be user interface and workflow enhancements that promise to make Houdini much more artist-friendly. Also, the new release will include low-level integration of Python for added power and flexibility")

print dir(synth)


