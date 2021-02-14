--Stored Procedures for Employee table

--Stored Procedure of Insert Record
create proc Sp_InsertEmpRecord
@eName varchar(20),
@eSalary float,
@deptno int
as begin
insert into Employees(EmpName,Salary,DeptNo) values(@eName,@eSalary,@deptno)
end

execute Sp_InsertEmpRecord 'prachi',23232,102

--Stored Procedure of Update Record
create proc Sp_UpdateEmpRecord
@eid int,
@eName varchar(20),
@eSalary float,
@deptno int
as begin
update Employees set EmpName=@eName,Salary=@eSalary,DeptNo=@deptno where EmpId=@eid
end

execute Sp_UpdateEmpRecord 1002,'prachi solanki',2323.2,102

--Stored Procedure of Delete Record
create proc Sp_DeleteEmpRecord
@eid int
as begin
delete Employees where EmpId=@eid
end

execute Sp_DeleteEmpRecord 1003