

import sys
import os


print "Engine Builder"

RULE_MATCH_STOP = 0x00
RULE_MATCH_CONTINUE = 0x01


class Rule :

    """
    The Rule class just stores the information about the rule
    """

    def __init__( self , name, func , true_action ) :
        self.name = name
        self.func = func
        self.true_action = true_action
        self.matched_rules=[]

    def __str__( self ) :
        s = "Rule " + self.name + " "
        return s

class ActionDef :
    
    def __init__( self , name, stop ) :
        self.name = name
        self.stop = stop

class EvalResult :

    def __init__( self ) :
        pass
        self.action = None
        self.stopping_rule = None

    def __str__( self ) :
        s = "EvalResult" + self.action
        return s

    def dump( self ) :
        print
        print "Result"
        print "------"
        print "action =",self.action
        print "stopping rule =",self.stopping_rule
        print "matching rules ", self.matched_rules

class Engine :

    """
    This is the fundamental engine class. Use it to create specific engines as needed.

    WORKITEMS
    ---------
    dump() - produce a nice representation of the engine

    Create a EvaluationResult class
    
    """

    def __init__( self ) :
        self.rules = []
        self.rules_dic = {}
        self.default_action = None
        self.actiondefs = {}
        self.execution_context = {}

    def __str__( self ) :
        s = "RuleEngine"
        return s

        
    def define_action( self, name, stop ) :
        assert( len(name) > 0 )
        assert( stop in [ RULE_MATCH_STOP , RULE_MATCH_CONTINUE ] )
        ad = ActionDef( name, stop )
        self.actiondefs[name] = ad

    def add_rule( self, rulename, condition, action ) :
        
        if ( rulename in self.rules_dic ) :
            print "Rule with same name exists"
            sys.exit()
            

        if ( action not in self.actiondefs ) :
            print "Action", action, " in rule", rulename,"is not defined"
            sys.exit()
        
        rule = Rule(rulename, condition , action )
        self.rules.append( rule )
        self.rules_dic[ rulename ] = rule        

    def evaluate( self, input ) :
        #print
        #print "Evalution"
        #print "---------"
        #print "Input Event:", " ".join( [ ("%s=%s"%(k,v)) for k,v in input.iteritems() ] )

        res = EvalResult()
        res.action = None
        res.stopping_rule = None

        matched_rules = []
        rule_action = None

        #print
        #print "rules evalutation:"    
        for rule in self.rules :
            
            result = eval( rule.func, self.execution_context, input )            
            #print "", rule.name, "->", result
            if ( result == True ) :
                matched_rules.append( rule.name )
                rule_action = rule.true_action
                action_def  = self.actiondefs[ rule_action ]
                if (action_def.stop == RULE_MATCH_STOP) :
                    res.stopping_rule = rule
                    break



        if (rule_action==None) :
            # No Rule fired
            res.action = self.default_action
        else :
            # rule fired
            res.action = rule_action

        res.matched_rules = matched_rules[:]
        
        return res

    def dump(self) :
        print "Execution Context"
        for k,v in self.execution_context.iteritems() :
            if ( k != '__builtins__') :
                print "  " + k + " = " + str(v)
        print "Rules Loaded in Engine"
        for rule in self.rules :
            print "  " + rule.name + " if (" + rule.func + ") then "+rule.true_action
            

def create_simple_allow_block_engine( def_action ) :    
    e = Engine()
    e.define_action( "ALLOW", RULE_MATCH_STOP )
    e.define_action( "BLOCK", RULE_MATCH_STOP )
    e.default_action = def_action
    return e



def test1() :

    e1 = create_simple_allow_block_engine( "BLOCK" )
    e1.add_rule( "msblessed", "(site in microsoft_blessed_sites) and (protocol in [ 'web', 'ftp' ]) " , "ALLOW" )

    e1.add_rule( "adminbannedsites", "site in admin_banned_sites" , "BLOCK" )
    e1.add_rule( "adminbannedcontent", "content in admin_banned_content" , "BLOCK" )
    e1.add_rule( "adminblessedsites", "site in admin_blessed_sites" , "ALLOW" )

    e1.add_rule( "blockedusers", "user in admin_blocked_users" , "BLOCK" )
    
    e1.add_rule( "web access", 'protocol=="web"' , "ALLOW" )

    e1.execution_context[ "microsoft_blessed_sites" ] = [ 'update.microsoft.com', 'windowsupdate.microsoft.com']
    e1.execution_context[ "admin_banned_sites" ] = [ 'bannedsite.com']
    e1.execution_context[ "admin_blessed_sites" ] = [ 'blessedsite.com']
    e1.execution_context[ "admin_blocked_users" ] = [ 'badguy']
    e1.execution_context[ "admin_blocked_outbound_protocols" ] = [ 'ftp']
    e1.execution_context[ "admin_banned_content" ] = [ 'wmf']
    e1.dump()

    print
    print
    result = e1.evaluate( dict(site='update.microsoft.com', protocol='web', user="bill", content='html') )
    result.dump() # should be allowed 

    result = e1.evaluate( dict(site='foo.com', protocol='web', user="bill", content='html') )
    result.dump() # should be allowed

    result = e1.evaluate( dict(site='foo.com', protocol='web', user="badguy", content='html') )
    result.dump() # should be blocked

    result = e1.evaluate( dict(site='foo.com', protocol='ftp', user="bill", content='html') )
    result.dump()  # should be blocked

    result = e1.evaluate( dict(site='foo.com', protocol='web', user="bill", content='wmf') )
    result.dump()  # should be blocked

    #result = e1.evaluate( dict(site=0, y=1000, z=100) )
    #result.dump()

    test_sites = [ 'update.microsoft.com', 'unknown.com', 'bannedsite.com', 'blessedsite.com' ]
    test_users = [ 'blockeduser', 'unknownuser', 'approveduser' ]
    test_content= [ 'wmf', 'html', 'unknowncontent' ]
    test_localapp = [ 'blockedapp', 'approvedapp', 'unknownapp' ]
    test_protocol = [ 'web', 'ftp', 'unknownapp' ]
    for t in  ( (site,user, content, localapp, protocol) for site in test_sites  for user in test_users for content in test_content for localapp in test_localapp for protocol in test_protocol ) :
        (site,user, content, localapp, protocol)  = t
        print t
        dic = { 'site':site, 'user':user , 'protocol': protocol, 'content':content}
        result = e1.evaluate( dic )
        if ( site in e1.execution_context[ "microsoft_blessed_sites" ] ) :
            if (result.action == 'BLOCK' ) :
                print t
                raise "ERROR"
        
    
if ( __name__=='__main__' ) :
    test1()
    