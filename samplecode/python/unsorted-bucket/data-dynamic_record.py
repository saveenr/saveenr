
class Record :
    
    def __init__(self, **kw):
        self.__dict__ = kw
