# Written with pymongo-3.2 
# Documentation: http://api.mongodb.org/python/
# A python script connecting to a MongoDB given a MongoDB Connection URI.

import sys
import pymongo

### Standard URI format: mongodb://[dbuser:dbpassword@]host:port/dbname

MONGODB_URI = 'mongodb://qtp:qtp@ds023118.mlab.com:23118/quote' 

###############################################################################
# main
###############################################################################

def main(args):

    client = pymongo.MongoClient(MONGODB_URI)
    db = client.get_default_database()
    cursor = db.tick.find({})

    print ('all tick count: %d' %(cursor.count()))
    
    ### Only close the connection when your app is terminating

    client.close()


if __name__ == '__main__':
    main(sys.argv[1:])