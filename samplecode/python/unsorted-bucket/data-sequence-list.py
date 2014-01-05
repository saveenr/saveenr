
import types
import string
from range import Range

import types

class Range :

	
	def __init__( self, lower, upper ) :
		self.lower=lower
		self.upper=upper
		self.check_number_type(self.lower)
		self.check_number_type(self.upper)
		if (self.lower>self.upper) :
			raise "BASS ACKWARDS"
	
	def __del__( self ) :
		pass
	
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

def read_range_from_string( s ) :
	list = string.split(s,'-')
	n = len(list)
	if (n==2) :
		return ( Range( string.atoi(list[0]), string.atoi(list[1])) )
	elif (n==1) :
		return ( Range(string.atoi(list[0]),string.atoi(list[0])) )
	else :
		raise "ERROR"

def get_regular_range_list( start, width, count ) :
	list = []
	for i in xrange(count) :
		abs_start = start + (i * width )
		r = Range( abs_start, abs_start+width-1 )
		list.append( r )
	return list


RangeType = type(Range(0,1))



class SequenceList :

	def __init__( self ) :
		self.list = [ ]
	
	def __del__( self ) :
		del (selflist)
	
	def __str__(self) :
		s= ''
		count = 0
		for range in self.list :			
			if (count>0) :
				s=s+','			
			s = s + str(range)	
			count = count + 1
		return s

	def add( self, arg ) :	
		self.check_arg_and_send( arg , self.add_range )

		t = type(arg)
		if (t == types.IntType ) :
			self.add_range(Range(arg,arg))
		elif (t==RangeType) :
			self.add_range(arg)
		elif ( t == types.ListType ) :
			for item in arg :
				self.add(item)
		else :
			raise "UNSUPORTED TYPE"

	def check_arg_and_send( self, arg , func ) 	:
		t = type(arg)
		if (t == types.IntType ) :
			func(Range(arg,arg))
		elif (t==RangeType) :
			func(arg)
		elif ( t == types.ListType ) :
			for item in arg :
				self.check_arg_and_send(item,func)
		else :
			raise "UNSUPORTED TYPE"
	
	def min( self ) :
		if ( len(self.list) <1 ) :
			raise "EMPTY LIST"
			
		return ( self.list[0].lower)
	
	def max( self ) :
		if ( len(self.list) <1 ) :
			raise "EMPTY LIST"
			
		return ( self.list[-1].upper)
		
	def contains( self , n ) :
		self.check_number_type(n)
		range, index = self.find_range_containing_number(n)
		if (tuple) :
			return 1
		else :
			return 0

	def __len__(self) :
		n = 0
		for rng in self.list :
			n = n + len(rng)
		return n
		
	def add_range( self, new_rng ) :				
		left , right = [] , []
				
		for i in range(len(self.list)) :
			rng = self.list[i]
			if ( new_rng.intersects(rng) or new_rng.touches(rng)) :
				new_rng.joinwith(rng)
			elif ( rng.upper < new_rng.lower ) :
				left.append(rng)
			elif ( new_rng.upper < rng.lower ) :
				pass
				right.append(rng)
			else :
				raise "SHOULD NEVER GET HERE", (rng,new_rng)

		self.list = left + [ new_rng] + right			
		
	def find_range_containing_number( self, n ) :
		index = 0
		for rng in self.list :
			if (rng.contains_number(n)) :
				return (rng, index)
			index = index + 1
		return (None,None)
					
	def remove( self , n ) :
		self.check_arg_and_send( arg , self.remove_range )

	def remove_range( self, the_rng ) :
		
		middle = []
		t_index = None
		
		for i in range(len(self.list)) :
			rng = self.list[i]
			if ( the_rng.intersects(rng) ) :
				if (t_index==None) :
					t_index = i		
				if ( the_rng.lower >= rng.lower) :
					if (rng.lower<=(the_rng.lower-1) ) :
						middle.append( Range( rng.lower, the_rng.lower -1 ) )
				if ( the_rng.upper <= rng.upper ) :
					if ((the_rng.upper+1)<=rng.upper ) :
						middle.append( Range(the_rng.upper+1,rng.upper) )
			elif ( rng.upper < the_rng.lower ) :
				pass
			elif ( the_rng.upper < rng.lower ) :
				pass
			else :
				raise "SHOULD NEVER GET HERE", (rng,new_rng)
				
		if (t_index!=None) :
			self.list[ t_index:(t_index+len(middle)) ] = middle

def read_sequence_list_from_string ( s ) :
	list = string.split(s,',')
	s = SequenceList()
	for item in list :
		r = read_range_from_string(item)
		s.add( r )
	return (s)		

