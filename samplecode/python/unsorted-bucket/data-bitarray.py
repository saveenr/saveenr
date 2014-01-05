
import os
import string

def get_2_power( n ) :
	return ( 1L << n )

def get_2_sum( n ) :
	return ( get_2_power(n) - 1 )

# ----------------------------------------
#
# @class BitArray
#
# Implements an arbitrary length array of bits
#
# ----------------------------------------

class BitArray :

	def __init__( self , value = 0L ) :
		self.value = value

	def __del__(self) :
		pass

	def __str__(self) :
		return ( hex(self.value) )

	def get_bit( self, index ) :
		return ( (self.value & ( 1 << index ) ) >> index)

	def set_bit( self , index ) :
		self.value = ( self.value | (1 << index ) )

	def clear_bit( self , index ) :
		self.value = ( self.value & ( ~ (1 << index ) ) )

	def clear_all_bits( self ) :
		self.value = 0x00L

	def __cmp__( self, b ) :
		if (self.value<b.value) : return -1
		elif (self.value>b.value) : return 1
		else : return 0





