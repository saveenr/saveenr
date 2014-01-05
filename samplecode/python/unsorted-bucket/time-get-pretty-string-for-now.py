import time

"""

a pretty time string to use for filenames

"""

if ( __name__ == "__main__" ) :

    timestring = time.strftime("(%Y-%m-%d--%H-%M-%S%p-%a)", time.localtime())
    print timestring


