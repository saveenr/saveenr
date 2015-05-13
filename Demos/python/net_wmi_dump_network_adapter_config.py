import os
import sys

import wmi


def dump_network_adapter_config():

    computer = wmi.WMI ()
    for nic in computer.Win32_NetworkAdapterConfiguration (IPEnabled=1):
        print 
        print "Hostname: ", nic.DNSHostName
        print "NIC: ", nic.Description
        print "DHCP: ", nic.DHCPEnabled 
        print "Mac Address: ", nic.MACAddress
        for ip_address in nic.IPAddress:
            print "IP Address: ", ip_address
        for addr in nic.DefaultIPGateway :
            print "Default Gateway: ", addr
        print "DNS Domain: ", nic.DNSDomain
        print

if ( __name__ == '__main__' ) :

    dump_network_adapter_config()    