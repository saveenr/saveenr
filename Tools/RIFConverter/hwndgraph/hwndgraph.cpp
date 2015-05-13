// hwndgraph.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
//#include <windows.h>

#include <exception>
#include "string.h"
#include "tchar.h"

#include "../mslib/tstring.h"
#include "../mslib/fsutil.h"
#include "../mslib/windowutil.h"
#include <list>
#include <map>

using mslib::TString;

class X
{

	std::list < HWND > hwnd_list_1;
	std::list < HWND > hwnd_list_2;
	std::map< HWND, int > hwnd_map;

public:
	void xprint_info( HWND hwnd)
	{
		CString classname = mslib::GetWindowClass(hwnd);
		CString title = mslib::GetWindowTitle(hwnd);
		printf("<%lX> [%s] \"%s\"", hwnd, (const char *)classname, (const char *)title );
	}
	void print_info( HWND hwnd)
	{
		xprint_info( hwnd  );
		//printf(" PARENT " );
		//xprint_info( ::GetParent( hwnd  ) );
		printf(" \n ");
	}

	int run()
	{

		// in order to get all the windows it seems like the hwnd below had to be getDesktopWindow???why?

		get_all_hwnds_1( ::GetDesktopWindow() );

		std::list< HWND >::iterator I;
		
		/*
		for (I=hwnd_list_1.begin(); I!=hwnd_list_1.end(); ++I)
		{
			print_info( *I);
		}
*/

		printf("\n\n\n\n\n");

		// you can change the hwnd below to be the target hwnd
		print2( 0 , 0);





		return 0;
	}

	void get_all_hwnds_1( HWND hwnd)
	{

		std::list< HWND > L;
		std::list< HWND >::iterator I;
		mslib::GetChildWindows( hwnd , &hwnd_list_1);

	}


	void print2( HWND hwnd , int depth )
	{
		for (int i=0;i<depth;i++) { printf("   "); }

		std::list< HWND >::iterator I;
		print_info( hwnd );
		for (I=hwnd_list_1.begin(); I!=hwnd_list_1.end(); ++I)
		{
			if (hwnd==::GetParent(*I))
			{
				print2( *I , depth + 1 );
			}
		}


	}
};


int main(int argc, char* argv[])
{


	printf( "\n");

	X x;
	x.run();

	return 0;
}

