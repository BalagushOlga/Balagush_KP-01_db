from Model import Sections
from Model import Categories
from Model import Subscriptions
from Model import Orders
from Model import Products
from View import View

v = View()
s = Sections()
c = Categories()
sub = Subscriptions()
o = Orders()
p = Products()

cont = True
while(cont):
    com = v.readCommand()
    if (com == 'exit'):
        cont = False
    elif (com == 'create' or com == 'delete' or com == 'update'):
        tab = v.readTable()
        if(com == 'delete' and tab == 'sections'):
            id = v.getNum()
            s.delete(id)
        elif(com == 'create' and tab == 'sections'):
            id = v.getNum()
            section_name = v.getStr()
            s.create(id, section_name)
        elif(com == 'update' and tab == 'sections'):
            id = v.getNum()
            section_name = v.getStr()
            s.update(id, section_name)
        elif(com == 'delete' and tab == 'categories'):
            id = v.getNum()
            c.delete(id)
        elif(com == 'create' and tab == 'categories'):
            id = v.getNum()
            category_name = v.getStr()
            section_id = v.getNum()
            c.create(id, category_name, section_id)
        elif(com == 'update' and tab == 'categories'):
            id = v.getNum()
            category_name = v.getStr()
            section_id = v.getNum()
            c.update(id, category_name, section_id)
        elif(com == 'delete' and tab == 'subscriptions'):
            id = v.getNum()
            sub.delete(id)
        elif(com == 'create' and tab == 'subscriptions'):
            id = v.getNum()
            order_id = v.getNum()
            product_id = v.getNum()
            sub.create(id, order_id, product_id)
        elif(com == 'update' and tab == 'subscriptions'):
            id = v.getNum()
            name = v.getStr()
            order_id = v.getStr()
            product_id = v.getNum()
            sub.update(id, order_id, product_id)
        elif(com == 'delete' and tab == 'orders'):
            id = v.getNum()
            o.delete(id)
        elif(com == 'create' and tab == 'orders'):
            id = v.getNum()
            customer = v.getStr()
            price = v.getNum()
            o.create(id, customer, price)
        elif(com == 'update' and tab == 'orders'):
            id = v.getNum()
            customer = v.getStr()
            price = v.getNum()
            o.update(id, customer, price)
        elif(com == 'delete' and tab == 'products'):
            id = v.getNum()
            p.delete(id)
        elif(com == 'create' and tab == 'products'):
            id = v.getNum()
            product_name = v.getStr()
            price = v.getNum()
            category_id = v.getNum()
            p.create(id, product_name, price, category_id)
        elif(com == 'update' and tab == 'products'):
            id = v.getNum()
            product_name = v.getStr()
            price = v.getNum()
            category_id = v.getNum()
            p.update(id, product_name, price, category_id)
        else:
            print('Invalid values')
    else:    
        print('Invalid values')