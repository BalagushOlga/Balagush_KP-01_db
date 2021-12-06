import timeit
import psycopg2
from psycopg2 import Error
from sqlalchemy import create_engine, Integer, String, \
    Column, ForeignKey
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import session, sessionmaker, relationship

Base = declarative_base()

engine = create_engine("postgresql+psycopg2://postgres:1@localhost/lab1shop")
Base.metadata.bind = engine
Base.metadata.create_all(engine)
Session = sessionmaker(bind=engine)
engine.connect()

class Section(Base):
    __tablename__ = 'sections'
    id = Column(Integer, primary_key=True, unique=True, nullable=False)
    section_name = Column(String(50))
    __table_args__ = {'extend_existing': True}

class Category(Base):
    __tablename__ = 'categories'
    id = Column(Integer, primary_key=True, unique=True, nullable=False)  
    category_name = Column(String(50))
    section_id = Column(Integer, ForeignKey('sections.id'))
    __table_args__ = {'extend_existing': True}

class Subscription(Base):
    __tablename__ = 'subscriptions_o_p'
    id = Column(Integer, primary_key=True, unique=True, nullable=False)
    order_id = Column(Integer, ForeignKey('orders.id'))
    product_id = Column(Integer, ForeignKey('products.id'))
    __table_args__ = {'extend_existing': True}

class Order(Base):
    __tablename__ = 'orders'
    id = Column(Integer, primary_key=True, unique=True, nullable=False)
    customer = Column(String(50))
    price = Column(Integer)
    __table_args__ = {'extend_existing': True}

class Product(Base):
    __tablename__ = 'products'
    id = Column(Integer, primary_key=True, unique=True, nullable=False)
    product_name = Column(String(50))
    price = Column(Integer)
    category_id = Column(Integer, ForeignKey('catecories.id'))
    __table_args__ = {'extend_existing': True}

class Sections:

    def __init__(self):  
        self.id = 0  
        self.section_name = ""

    def create(self, id, section_name):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            session = Session()
            session.add(Section(
                id = id,
                section_name = section_name
            ))
            session.commit()
            print("Entity inserted")

        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
    
    def update(self, id, section_name):
        if (id < 1):
            print('Error with input!')
            return  
        try:
            i = session.query(Section).get(id)
            i.section_name = section_name
            session.add(i)
            session.commit()
            print("Entity updated")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

    def delete(self, id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            i = session.query(Section).get(44)
            session.delete(i)
            session.commit()
            print("Entity deleted")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
        
class Categories:

    def __init__(self):  
        self.id = 0  
        self.category_name = ""  
        self.section_id = 0  

    def create(self, id, category_name, section_id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            session = Session()
            session.add(Category(
                id = id,
                category_name = category_name,
                section_id = section_id
            ))
            session.commit()
            print("Entity inserted")

        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
    
    def update(self, id, category_name, section_id):
        if (id < 1):
            print('Error with input!')
            return  
        try:
            i = session.query(Category).get(id)
            i.category_name = category_name,
            i.section_id = section_id
            session.add(i)
            session.commit()
            print("Entity updated")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

    def delete(self, id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            i = session.query(Category).get(id)
            session.delete(i)
            session.commit()
            print("Entity deleted")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
        
class Subscriptions:

    def __init__(self):  
        self.id = 0  
        self.order_id = 0
        self.product_id = 0
    def create(self, id, order_id, product_id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            session = Session()
            session.add(Subscription(
                id = id,
                order_id = order_id,
                product_id = product_id
            ))
            session.commit()
            print("Entity inserted")

        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
    
    def update(self, id, order_id, product_id):
        if (id < 1):
            print('Error with input!')
            return  
        try:
            i = session.query(Subscription).get(id)
            i.order_id = order_id
            i.product_id = product_id
            session.add(i)
            session.commit()
            print("Entity updated")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

    def delete(self, id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            i = session.query(Subscription).get(id)
            session.delete(i)
            session.commit()
            print("Entity deleted")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

class Orders:

    def __init__(self):  
        self.id = 0  
        self.customer = ""  
        self.price = 0 

    def create(self, id, customer, price):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            session = Session()
            session.add(Order(
                id = id,
                customer = customer,
                price = price
            ))
            print("Entity inserted")
            session.commit()
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
    
    def update(self, id, customer, price):
        if (id < 1):
            print('Error with input!')
            return  
        try:
            i = session.query(Order).get(id)
            i.customer = customer,
            i.price = price
            session.add(i)
            session.commit()
            print("Entity updated")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

    def delete(self, id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            i = session.query(Order).get(id)
            session.delete(i)
            session.commit()
            print("Entity deleted")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

class Products:
    
    def __init__(self):  
        self.id = 0  
        self.product_name = ""  
        self.price = 0 
        self.category_id = 0 

    def create(self, id, product_name, price, category_id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            session = Session()
            session.add(Product(
                id = id,
                product_name = product_name,
                price = price,
                category_id = category_id
            ))
            print("Entity inserted")
            session.commit()
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()
    
    def update(self, id, product_name, price, category_id):
        if (id < 1):
            print('Error with input!')
            return  
        try:
            i = session.query(Product).get(id)
            i.product_name = product_name,
            i.price = price,
            i.category_id = category_id
            session.add(i)
            session.commit()
            print("Entity updated")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

    def delete(self, id):
        if (id < 1):
            print('Error with input!')
            return 
        try:
            i = session.query(Product).get(id)
            session.delete(i)
            session.commit()
            print("Entity deleted")
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            print()

#Hash, BRIN
class Index:

    def test(self):
        start = timeit.timeit()
        try:
            connection = psycopg2.connect(user="postgres",
                                            password="1a2s3d4f",
                                            host="localhost",
                                            port="5433",
                                            database="lab1shop")
            cursor = connection.cursor()
            selecr_query = """CREATE INDEX ON Sections USING HASH(id);
                            CREATE INDEX ix_id ON Categories USING BRING(id);"""
            cursor.execute(selecr_query)
            connection.commit()
            print("Result", cursor.fetchall())
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            if connection:
                cursor.close()
                connection.close()
                end = timeit.timeit()
                print("Time for operation " + str(end - start))
#after insert, update
class Trigger:

    def create(self):
        try:
            connection = psycopg2.connect(user="postgres",
                                            password="1a2s3d4f",
                                            host="localhost",
                                            port="5433",
                                            database="lab1shop")
            cursor = connection.cursor()
            query = """DROP TABLE IF EXISTS Subscriptions;
                        CREATE TABLE Subscriptions(order_id integer, product_id integer);
                        CREATE OR REPLACE FUNCTION Subscriptions() RETURNS trigger AS $BODY$
                        BEGIN
                            IF NEW.order_id IS NULL THEN
                                RAISE EXCEPTION 'Order cannot have null id';
                            END IF;
                            IF NEW.product_id IS NULL THEN
                                RAISE EXCEPTION 'Product cannot have null id';
                            END IF;
                            INSERT INTO subj_logs VALUES(NEW.order_id, NEW.product_id);
                            RETURN NEW;
                        END;
                    $BODY$ LANGUAGE plpgsql;
                    DROP TRIGGER IF EXISTS order_id ON Subscriptions;
                    CREATE TRIGGER order_id AFTER INSERT OR UPDATE ON Subscriptions
                        FOR EACH ROW EXECUTE PROCEDURE order_id();"""
            cursor.execute(query)
            connection.commit()
        except (Exception, Error) as error:
            print("Error with PostgreSQL", error)
        finally:
            if connection:
                cursor.close()
                connection.close() 