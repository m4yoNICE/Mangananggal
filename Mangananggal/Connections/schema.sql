-- Create the database
CREATE DATABASE mangananggalDB;
GO

-- Use the database
USE mangananggalDB;
GO

-- Create the users table
CREATE TABLE users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    user_username VARCHAR(50) NOT NULL,
    user_password VARCHAR(255) NOT NULL,
    user_firstname VARCHAR(50),
    user_lastname VARCHAR(50),
    user_role VARCHAR(20)
);
GO

-- Create the otp table
CREATE TABLE otp (
    otp_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    otp_code VARCHAR(10) NOT NULL,
    otp_expiration DATETIME NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
GO

-- Create the books table
CREATE TABLE books (
    book_id INT PRIMARY KEY IDENTITY(1,1),
    book_name VARCHAR(100) NOT NULL,
    book_author VARCHAR(100),
    book_genre VARCHAR(50),
    book_price DECIMAL(10, 2) NOT NULL
);
GO

-- Create the orders table
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
GO

-- Create the order_items table
CREATE TABLE order_items (
    item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT NOT NULL,
    product_id INT NOT NULL, -- This assumes a generic 'product_id' - maybe meant to link to 'books.book_id'?
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
    -- You may also want to reference books(book_id) if product_id refers to books
);
GO
