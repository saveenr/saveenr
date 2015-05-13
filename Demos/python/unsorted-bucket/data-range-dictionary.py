import sys
import os
import string
import types
import random
import exceptions

no_matching_range_exc = exceptions.Exception( 'No Matching Range' )
unsupported_type = exceptions.Exception( 'Unsupported type' )

import types

class Range :

	
	def __init__( self, lower, upper ) :
		self.lower=lower
		self.upper=upper
		self.check_number_type(self.lower)
		self.check_number_type(self.upper)
		assert( self.lower <= self.upper )
	
	def __del__( self ) :
		pass
	
	def __repr__(self) :
		return '%d-%d' % (self.lower,self.upper)

	def __str__(self) :
		return '%d-%d' % (self.lower,self.upper)
	
	def __len__(self ):
		return ( self.upper - self.lower + 1)
	
	def intersects( self , rng ) :
		def func( a, b ) :
			return ( (a.lower<=b.lower) and (a.upper>=b.lower) )  
		return ( func(self,rng) or func(rng	,self) )
	
	def touches( self, rng ) :
		def func(a,b) :
			return ( (a.upper+1)==b.lower ) 		
		return ( func(self,rng) or func(rng,self) )

	def check_number_type( self , n ) :
		t = type(n)
		if ( (t==types.IntType) ) :
			pass
		else :
			raise "TYPEERROR", t

	def contains_number( self, 	n ) :
		return ( self.lower <= n <= self.upper )
					
	def joinwith( self, rng ) :
		if ( self.intersects(rng) or self.touches(rng) ) :			
			self.upper = max( self.upper, rng.upper )
			self.lower = min( self.lower, rng.lower )
		else :
			raise "THEY DONT' INTERSECT OR TOUCH"		



RangeType = type(Range(0,1))



class GenericValueDistribution :

	def __init__( self ) :
		self.dic = {}


	def __del__( self ) :
		pass

	def AddRange( self,arg) :
		t = type(arg)
		if (t == RangeType ) :
			self.dic[ arg ] = 0
		elif (t==types.ListType) :
			for r in arg :
				self.AddRange( r )
		else :
			raise unsupported_type

	def AddValue( self, n ) :
		for r in self.dic.keys() :
			if r.contains_number( n ) :
				val = self.dic[ r ] +1
				self.dic[r] = val
				return
		else :
			raise no_matching_range_exc


def main() :

    D = GenericValueDistribution()
    D.AddRange( Range( 0, 9) )
    D.AddRange( Range( 10, 20) )

    for i in xrange(100) :
        r = int(random.random()*20);
        print r
        D.AddValue( r )

         
    print D.dic

main()
