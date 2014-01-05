# ----------------------------------------
# SCRIPT: add_user_to_local_group.py
#
# EXAMPLE: Add "domain\user1" to administrators group on local machine
# add_user_to_local_group.py "" domain\user1 Administrators
# add_user_to_local_group.py localhost domain\user1 Administrators
#
# NOTE: this is case-insensitive
# NOTE: requires win32 extensions for pythoon
#

import os
import win32net
import win32netcon
import sys

def add_user_to_local_group( computer, account, localgroup, ) :
    localgroup = unicode(localgroup).lower()
    groups = win32net.NetUserGetLocalGroups( computer, account, 0 )
    groups = [ s.lower() for s in groups ]

    if (localgroup in groups ) :
        #user is a already in the group
        pass
    else :
        #must add the user
        level =3
        members = []
        members.append( {"domainandname" : account } )
        win32net.NetLocalGroupAddMembers( computer, localgroup, level, members  )

    # postconditions
    groups = win32net.NetUserGetLocalGroups( computer, account, 0 )
    groups = [ s.lower() for s in groups ]
    assert( localgroup in groups )

the_computer = sys.argv[1]
the_account_name = sys.argv[2]
the_group = sys.argv[3]

add_user_to_local_group( the_computer, the_account_name , the_group, )


# ----------------------------------------
    