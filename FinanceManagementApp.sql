-- Database
CREATE DATABASE FinanceManagementSystem;
USE FinanceManagementSystem;

create table Users (
    user_id int primary key identity(1,1), 
    username varchar(50) not null unique,
    password varchar(255) not null,
    email varchar(100) not null unique
);

-- Create the expensecategories table
create table ExpenseCategories (
    category_id int primary key,
    category_name varchar(50) not null unique
);

-- Create the expenses table with expense_id as identity
create table Expenses (
    expense_id int primary key identity(1,1), 
    user_id int not null,
    amount int not null,
    category_id int not null,
    date date not null,
    description varchar(255),
    foreign key (user_id) references users(user_id),
    foreign key (category_id) references expensecategories(category_id)
);

-- Insert data into the users table
insert into users (username, password, email) values 
('anushansa', '12345', 'anushansapoonia@gmail.com'),
('jatin', '78609', 'jatinbhati@com');


-- Insert data into the expensecategories table
insert into expensecategories (category_id, category_name) values 
(1, 'food'),
(2, 'transportation'),
(3, 'utilities')
;

-- Insert data into the expenses table
insert into expenses (user_id, amount, category_id, date, description) values 
(1, 120, 1, '2024-09-23', 'cafe charge'),
(2, 59, 3, '2024-09-20', 'medical');

SELECT * FROM Expenses;
SELECT * FROM Users;
SELECT * FROM ExpenseCategories;