create table Rides
(
	Ride_ID int Primary Key identity(1,1),
	Ride_Description varchar(50),
	Ride_Availability bit,
	Ride_Cost int,
	Ride_Length int,
	Ride_Photo_Name varchar(50)
);


create table Employees
(
	Employee_ID int Primary Key identity(1,1),
	Employee_Name varchar(50),
	Employee_Surname varchar(50),
	Employee_Emergency_Contact varchar(50),
	Employee_Contact varchar(50),
	Employee_Password varchar(50),
	Ride_ID int foreign key (Ride_ID) references Rides(Ride_ID)
);


create table Customers
(
	Customer_ID int Primary Key identity(1,1),
	Customer_Name varchar(50),
	Customer_Surname varchar(50),
	Customer_DOB Date,
	Customer_Contact varchar(50),
	Customer_Email varchar(50),
);


create table Transactions
(
	Transaction_ID int Primary Key identity(1,1),
	Transaction_Date Date,
	Transaction_Time Time,
	Transaction_Amount money,
	Ride_ID int foreign key (Ride_ID) references Rides(Ride_ID),
	Customer_ID int foreign key (Customer_ID) references Customers(Customer_ID)
);