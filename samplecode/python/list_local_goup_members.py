# ------------------------------
# SCRIPT: list_local_group_members.
#
# EXAMPLE:
#  list_local_group_members.py
#
# NOTE: requires win32 extensions for python
#

import os
import win32net
import win32netcon
import pprint

def get_members_of_local_group( computername, groupname ) :
    resume = 0
    level = 2
    retval = []
    while (1) :
        data, total, resume=win32net.NetLocalGroupGetMembers( computername , groupname,2,resume)
        retval.extend( data )
        if (resume==0) :break
    return retval

the_computer = os.environ['computername']
the_group = 'Administrators'
users = get_members_of_local_group( the_computer, the_group)

pprint.pprint(users)

# ------------------------------