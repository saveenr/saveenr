# PowerShell / C# / Python Translation Guide


Updated on 2015/05/20


# Basics				
---

###True/False				


```
C#
    true
    false

Python
    True
    False		

PowerShell
    $true
    $false		

```

###Null				


```
C#
    null		

Python 
    None		

PowerShell 2.0	
    $null		
```

### Declare and Assign				


```

C#
    var hello = "world";		

Python 
	hello = "world"		

PowerShell 2.0	
	$hello = "word"		
```

### if				

```
C#	
	if ( true ) { … }
	if ( false ) { … }
		
Python 
 	if ( True ) :
    	# do nothing
	if ( False ) :
    	# do nothing		

PowerShell 2.0	
	if ( $true ) { }
	if ( $false ) { }		
```


### test for null				

```
C# 3.0	
	if ( a == null ) { }		

Python or IronPython	
	if (a == None) :
    	# do nothing		

PowerShell 2.0	
	if ( $a –eq $null ) { }		
```

### do nothing				

```
C# 3.0			
	N/A


Python or IronPython	
    pass		


PowerShell 2.0			
	N/A
```
				
# File system				
---

### Get all files in a folder		

```
C# 3.0		
	Python or IronPython			

PowerShell 2.0	
	$files = Get-ChildItem		
```

### Get all files in a folder with a specific extension	


```
C# 3.0		

Python or IronPython			

PowerShell 2.0	
	$files = Get-ChildItem *.dll		
```

### Get all files in a folder recursively	


``` 
Python or IronPython			

PowerShell 
	2.0	$files = Get-ChildItem -Recurse		
```

### Get all files in a folder recursivelyand get full pathname	


```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	$files = Get-ChildItem –Recurse | Foreach-Object { $_.FullName}		
```

### Getting the My Documents Folder	C# 3.0			

```
Python or IronPython			

PowerShell 2.0
	[Environment]::GetFolderPath("MyDocuments")		
				
```				
				
# Paths				
---

### Splitting Paths	C# 

```
C# 3.0	
	var a = System.IO.Path.GetDirectoryName("foo\\bar\\beer")
	var b = System.IO.Path.GetFileName("foo\\bar\\beer")		
	

Python or IronPython	
	a , b = os.path.split ("foo\\bar\\beer" )		


PowerShell 2.0	$a = split-path "\foo\bar\beer"
	$b = split-path "\foo\bar\beer" -leaf
```

###	Joining Paths	

```
C# 3.0	
	System.IO.Path.Combine("foo\\bar" , "beer" )		

Python or IronPython	
	os.path.join( "foo\\bar" , "beer" )		

PowerShell 2.0	
	join-path "foo\bar" "beer"		
```			

### Path Exists?	


```
C# 3.0	
	if (System.IO.Directory.Exists("d:\\foo")) { }
	if (System.IO.File.Exists("d:\\foo.txt")) { }


Python or IronPython	
	if (os.path.exists( "d:\\foo.txt" )) :
    	# do something


PowerShell 2.0	
    if ( test-path "d:\foo.txt" ) { }
	if ( ! (test-path "D:\\x") ) { }

```		

### Get Full path			

```
C# 3.0	
	System.IO.Path.GetFullPath("foo.txt")	

Python or IronPython	
	os.path.abspath( "foo.txt" )		

PowerShell 2.0	
	[IO.Path]::GetFullPath("foo.txt")		
```

### Normalize Paths	

``` 
Python or IronPython			


PowerShell 2.0	
	$d = resolve-path "D:\foo\..\bar\beer"		
```

### Create a Directory				

```
PowerShell 2.0	
	New-Item -Path d:\folder -ItemType Directory		
```

### Delete Folder and Contents Recursively	

```
C# 3.0			


Python or IronPython			

PowerShell 2.0	
	Remove-Item $folder -Recurse		
				
```
				
# Loops				
---

### For	

```
C# 3.0	
	for (int i=0; i<10;i++) { }		


Python or IronPython	
    for i in xrange(10) :
       pass		


PowerShell 2.0	
    foreach ($i in 0..9)  { }		
```

### For Each

```
C# 3.0	
	var a = [1,7,8,10]
	foreach (var i in a) { }		

Python or IronPython	
    a = [1,7,8,10]
    for i in a:
       pass		

PowerShell 2.0	
	$a = @(1,2,3)
	foreach ($i in $a) { }		
```

### While	


```
C# 3.0	
	while (true) { }		

Python or IronPython	
	while (True) :
    	pass		

PowerShell 2.0	
	while ($true) { }
```		
				
# Lists				
---


### New List (untyped)	


```
C# 3.0	
	var names = new List<object> { "Foo, "Bar", "Beer" };	

Python or IronPython	
	names = [ "Foo", "Bar", "Beer" ]		

PowerShell 2.0	
	$names =@( "Foo", "Bar", "Beer" )
or
	$names = "Foo", "Bar", "Beer"		
```

### New List (typed)

```
C# 3.0	
	var names = new List<string> { "Foo, "Bar", "Beer" };		

Python or IronPython	
	N/A		

PowerShell 2.0	
	[string[]] $names = @( "Foo", "Bar", "Beer" )
```		
				
### New empty list (untyped)

```
C# 3.0	
	var names = new List<object> { };		

Python or IronPython	
	names = []

PowerShell 2.0	
	$names =@( )		
```

### New empty list (typed)	


```
C# 3.0	
	var names = new List<string> { };		

Python or IronPython	
	N/A
		
PowerShell 2.0	
	[string[]] $names =@( )		
```

### Length of a list	


```
C# 3.0	
	names.Count		

Python or IronPython	
	len(names)		

PowerShell 2.0	
	$names.Length		
```

# Tuples				
---

### Create a new Tuple


```
C# 3.0	
	var t = Tuple.Create("Foo", 1.7);		

Python or IronPython		
	t  = ( "Foo", 1.7 )		


PowerShell 2.0	
	N/A		
```				
# Arrays				
---

```
C# 3.0	
	var names = new int[] { 1, 2, 3 };		

Python or IronPython	
	# via the array module
	# you are better-off using python lists 99.99% percent of the time
	names = array.array('i', [1,2,3] )
		

PowerShell 2.0	
	N/A		
```
				
# Dictionaries				
---

### Empty Dictionary				

```
C# 3.0	
	using System.Collections.Generic;
	var d = new Dictionary<string,string>;		


Python or IronPython	
	d = { }
	d = dict()
		

PowerShell 2.0	
	$d = @{ }		
```

### Non-empty dictionary				



```
C# 3.0	
	var d = new Dictionary<string,string> 
   		{ {"foo","bar"} , {"baz","beer"}};		

Python or IronPython	
	d = { "foo" : "bar", "baz":"beer" }		

PowerShell 2.0	
	$d = @{"foo"="bar";"baz"="beer" }		
```

### set values				


```
C# 3.0	
	d[ "foo" ] = "bar2"		

Python or IronPython	
	d[ "foo" ] = "bar2"		

PowerShell 2.0	
	$d[ "foo" ] = "bar2"		
```

### key exists				



```
C# 3.0	
	if (d.ContainsKey("foo") { … }		

Python or IronPython	
	if ( "foo" in d ) :
    	#do something		

PowerShell 2.0	
	if $d.ContainsKey("foo") { }		
```

### get value				



```
C# 3.0	
	d[ "foo" ]		

Python or IronPython	
	d[ "foo" ]		

PowerShell 2.0	
	$d.Item("foo")		
```

get all keys				

```
C# 
	3.0	d.Keys		

Python or IronPython	
	d.keys()
or
	[k for k in d.iterkeys()]		

PowerShell 2.0	
	$d.Keys		
```

### get all values				


```
C# 3.0	
	d.Values		

Python or IronPython	
	d.values()
or
	[v for v in d.itervalues()]		

PowerShell 2.0	
	$d.Values		
```

# unique elements				

```
C# 3.0	
	var a = new [] { "a", "b", "c" };
	var b = a.Distinct().ToList();		

Python or IronPython	
	a = ["a", "a", "b"]
	b = set( a )		

PowerShell 2.0	
	$a = @( "a", "a", "b" ) 
	$b = a | sort –unique		
```

### zip				


``` 
C# 3.0	
	var a = new [] { 1, 2, 3};
	var b = new [] { "a", "b", "c" };
	var c = Enumerable.Range(a.Length)
    	.Select( i=> new { v0=a[i], v1=b[i] } )
    	.List();		

Python or IronPython	
	a = [1,2,3]
	b = ["a","b","c"]
	c = zip(a,b)		

PowerShell 2.0	
	$a = @( 1, 2, 3 ) 
	$b = @( "a", "b", "c" ) 
	$c = 0..($a.count - 1) | 
   		% {, ($a[$_], $b[$_])}  
```

# Queues

		
### Create A Queue	

```
PowerShell 2.0	
	$q = New-Object System.Collections.Queue		
```

# LINQ				
--

```
		var a = new [] { "a", "b", "c" };		
		a = ["a", "b", "c" ]		
		$a = @( "a", "b", "c" )		
```

### SELECT				

```
C# 3.0	
	var a1 = a.Select( x=>x.Length ).ToArray();		

Python or IronPython	
	a1 = [len(x) for x in a]		

PowerShell 2.0	
	$a1 = $a | ForEach-Object{ $_.Length }		
```

### WHERE				

```
C# 3.0	
	var a2 = a.Where( x=>x == "a" ).ToArray();		

Python or IronPython	
	a2 = [x for x in a if x=="a"]		

PowerShell 2.0	
	$a2 = $a | Where-Object { $a –eq "a" }		
```

### DISTINCT				

```
C# 3.0	
	var a3 = a.Distinct().ToArray();		

Python or IronPython	
	a3 = list( set( a ) )		

PowerShell 2.0	
	$a3 = $a | sort -unique		

```

### SUM on a property				

```
C# 3.0	
	int  s = a.Select( x=>x.Length).Sum();		

Python or IronPython	
	s = sum(len(x) for x in a)
or
	s = reduce( lambda x,y: x+y , a1)		

PowerShell 2.0	
	// incomplete		
```


### Selecting properties and Anonymous Types				

```
C# 3.0	
	var items2 = items.Select( item => new { a=item.Length, b=item.Name } );		

Python or IronPython	
	// incomplete		

PowerShell 2.0	
	$items2 = $items | Select-Object Length,Name 		
				
```			
				
### Add members				

```
C# 3.0	
	N/A		

Python or IronPython	
	N/A		

PowerShell 2.0	
	$obj1 | Add-Member -type NoteProperty -name PropName -Value "PropValue"		
```			

### Get List Of Property Values				

```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	get-service alerter | format-list -property *		
```
				
# Exceptions				
---

### Throwing a specific .NET Exception	

```
C# 3.0	
	throw new System.IO.DirectoryNotFoundException()		
	throw new System.IO.DirectoryNotFoundException("Could not find file FOO")		

Python or IronPython	
	raise System.IO.System.IO.DirectoryNotFoundException()		
	raise System.IO.System.IO.DirectoryNotFoundException("Could not find file FOO")		

PowerShell 2.0	
	throw (new-object System.IO.DirectoryNotFoundException)		
	throw (new-object System.IO.DirectoryNotFoundException "Could not find file FOO")		
```

				
# Classes				


### Create a new class				

```
C# 3.0	
	public class Record
	{
	   public string Name;
	}		

Python or IronPython	
	class Record :
	   def __init__(self) :
	        pass
		

PowerShell 2.0	

	add-type @"
	public class Record
	{
	   public string Name;
	}
	"@		
```
				
### Create an instance				

```
C# 3.0	
	var rec1 = new Record()	
	
Python or IronPython	
	rec1 = Record()		

PowerShell 2.0	
	$rec1 = new-object Record		
```
				
# Dates And Times				
---

### Get current Date and Time				

```
C# 3.0	
	var now = System.DateTime.Now		

Python or IronPython	
	import datetime
	now = datetime.datetime.now()		

PowerShell 2.0	
	$now = get-date		
```

### Create a Specific Date and Time				

```
C# 3.0	
	var t0 = new System.DateTime(1979,3,31)
	var t1 = new System.DateTime(1979,3,31,1,31,45)		

Python or IronPython	
	t0 = datetime.datetime(1979,3,31)
	t1 = datetime.datetime (1979,3,31,1,31,45)		

PowerShell 2.0	
	$t0 = Get-Date -Year 1979 -Month 3 -Day 31 
	$t1 = Get-Date -Year 1979 -Month 3 -Day 31 -Hour 1 -Minute 31 -Second 45
```
		
### parse a date time string				

```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	$t0 = get-date "1979/3/31"
	$t1 = get-date "1979/3/31 1:31:45am"		
```				
				
# Execution Environment				


###Get current directory				

```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	$d = Get-Location		
```

### Get location of current script or exe


```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	$d = Split-Path $MyInvocation.MyCommand.Path		
```

### What runtime is being used				

				
# Datatables				
---

Create a DataTable				
----------------------------------------

```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	$data1=New-ObjectSystem.Data.Datatable
```
	
### Define the Datatable Columns		
		
```
C# 3.0			

Python or IronPython			

PowerShell 2.0	

    $data1.Columns.Add("Value",[double])
    $data1.Columns.Add("Category",[string])
```
		
# Populating a datatable				

```
C# 3.0			

Python or IronPython			

PowerShell 2.0	
	$data1.Rows.Add(1,"A")
    $data1.Rows.Add(2,"B")
    $data1.Rows.Add(3,"C")
```		
				
# Other
---

### Warn When Using Uninitialized Variables	

```
    Set-StrictMode -Version 2
```

### Stop Script Execution If There Is an Error
```
    $ErrorActionPreference = "Stop"
```

### Windows Features
----------------------------------------

###(IMPORT WINDOWS MODULES FIRST)

    Get-Windows-Feature <featurename>
    Add-Windows-Feature <featurename>

### Windows Special Folders
----------------------------------------

### Location of My Documents
    $mydocs = [Environment]::GetFolderPath("MyDocuments")


### Get Installation Information about a Product

    get-wmiobject -query "select * from win32_product where IdentifyingNumber='{4676603E-5C2E-4FA4-815B-6A27C113A2C0}'"

### Find All the Products with a Specific Name

    get-wmiobject Win32_Product | Where-Object { $_.Name -like "IronPython*" }

# Misc
----------------------------------------


### Recording Your Session
    Start-Transcript -path d:\steps.ps1
    Stop-Transcript

### Unblocking Files Downloaded From the Internet

### For a single file
    Unblock-File foo.dll

### For all the files in a folder
    Dir d:\scopesdk | Unblock-File	

 



# Printing
----------------------------------------

### Add a printer 
    New-Object -ComObject WScript.Network).AddWindowsPrinterConnection("\\printerserver\hplaser3")

# System.Xml.Linq
----------------------------------------

### Loading the Assembly
    [Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq") | Out-Null

### Loading an XML File
    $xml = [System.Xml.Linq.XDocument]::Load( “c:\foo.xml” )


# Event Log
---


    $applog = Get-EventLog -LogName Application

### Counting Events by Source
    $applog | group Source | Sort-Object Count –Descending



