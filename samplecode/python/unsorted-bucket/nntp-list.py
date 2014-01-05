
import os
import sys

import nntplib

nntp_server = 'news.verizon.net'
nntp_username = "res02n9b"
nttp_password = "res02n9bqdo"

s = nntplib.NNTP( nntp_server ,user = nntp_username, password= nttp_password )

resp, count, first, last, name = s.group('comp.lang.python')

resp, subs = s.xhdr('subject', first + '-' + last)
for id, sub in subs[-10:]:
    print id, sub

print s