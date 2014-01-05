import email.MIMEText
import smtplib
 
# Here is the data needed for the message
data = "Hello World!" # This corresponds to body of the message
text_subtype = "plain"
text_encoding = "utf-8"
from_address = "foobar@foo.com"
to_address = "foobar@foo.com"
subject = "Greetings"

 
# Make sure the body is encoded correctly
data = data.encode( text_encoding) 
# Question: Should we encode the other headers also (to, from, subject, etc.)?
 
# Construct the text message object
msg = email.MIMEText.MIMEText( data, text_subtype,  text_encoding )
 
# Fill in the headers
msg["From"]=from_address
msg["To"]=to_address
msg["Subject"]=subject
 
# Send the message via SMTP
server = smtplib.SMTP('smarthost')
#server.set_debuglevel(1)
server.sendmail( from_address, [ to_address ] , msg.as_string() )
server.quit()
