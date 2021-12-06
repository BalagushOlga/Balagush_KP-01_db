class view:
    
    def __init__(self):  
        print('----------Welcome----------')

    def readCommand(self):
        com = input("Enter command (create, delete, update or exit): ")
        return com

    def readTable(self):        
        tab = input("Enter table name: ")
        if(tab == 'sections'or tab == 'categories' or tab == 'subscriptions_o_p' or tab == 'orders' or tab == 'products'):
            return tab
        return ''
    
    def getStr(self):
        val = input("Enter a value: ")
        return val

    def getNum(self):
        val = input("Enter a number: ")
        try:
            int(val)
        except ValueError:
            print(val + " isn't a number")
            return 0
        return int(val)