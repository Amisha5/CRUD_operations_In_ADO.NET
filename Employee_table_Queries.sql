--Employees Table
create table Employees(
EmpId int not null primary key identity(1,1),
EmpName varchar(20),
Salary float,
DeptNo int foreign key references  Departments(DeptId)
)

select * from Employees

--Departments Table
create table Departments(
DeptId int not null primary key,
DeptName varchar(20) not null
)

select * from Departments

--Insert Record in Departments table
insert into Departments values(101,'HR')
insert into Departments values(102,'Admin')
insert into Departments values(103,'Marketing')
insert into Departments values(104,'Sales')

--Insert Record In Employees Table
insert into Employees values('Amisha',22100.0,101)
insert into Employees values('Aman',32000.20,102)
insert into Employees values('Sai',23100.0,103)
insert into Employees values('Shalini',23000.20,104)
insert into Employees values('shirisha',22100.0,102)
insert into Employees values('Mohan',21050.20,103)
insert into Employees values('Jayanth',22400.0,101)
insert into Employees values('Naresh',21150.20,102)


