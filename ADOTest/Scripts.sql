CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[State](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Country_ID] [int] NOT NULL,
	[Abbr] [nchar](2) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [FK_State_Country] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Country] ([ID])
GO

ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_State_Country]
GO

CREATE TABLE [dbo].[PhoneType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Phone](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](15) NOT NULL,
	[Type_ID] [int] NOT NULL,
 CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Phone]  WITH CHECK ADD  CONSTRAINT [FK_Phone_PhoneType] FOREIGN KEY([Type_ID])
REFERENCES [dbo].[PhoneType] ([ID])
GO

ALTER TABLE [dbo].[Phone] CHECK CONSTRAINT [FK_Phone_PhoneType]
GO

CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Street] [nvarchar](200) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State_ID] [int] NOT NULL,
	[PostalCode] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_State] FOREIGN KEY([State_ID])
REFERENCES [dbo].[State] ([ID])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_State]
GO

CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FName] [nvarchar](20) NOT NULL,
	[LName] [nvarchar](30) NOT NULL,
	[Address_ID] [int] NOT NULL,
	[email] [nvarchar](200) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Address] FOREIGN KEY([Address_ID])
REFERENCES [dbo].[Address] ([ID])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Address]
GO


CREATE TABLE [dbo].[Customer_Phone](
	[Customer_ID] [int] NOT NULL,
	[Phone_ID] [int] NOT NULL,
 CONSTRAINT [PK_Customer_Phone] PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC,
	[Phone_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Customer_Phone]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Phone_Customer] FOREIGN KEY([Customer_ID])
REFERENCES [dbo].[Customer] ([ID])
GO

ALTER TABLE [dbo].[Customer_Phone] CHECK CONSTRAINT [FK_Customer_Phone_Customer]
GO

ALTER TABLE [dbo].[Customer_Phone]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Phone_Phone] FOREIGN KEY([Phone_ID])
REFERENCES [dbo].[Phone] ([ID])
GO

ALTER TABLE [dbo].[Customer_Phone] CHECK CONSTRAINT [FK_Customer_Phone_Phone]
GO


INSERT INTO PhoneType([Name]) VALUES('Office')
INSERT INTO PhoneType([Name]) VALUES('Home')
INSERT INTO PhoneType([Name]) VALUES('Cell')
INSERT INTO PhoneType([Name]) VALUES('Fax')

INSERT INTO Country([Name]) VALUES('United States')

INSERT INTO State([Name], Abbr, Country_ID) VALUES('Alabama','AL',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Alaska','AK',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Arizona','AZ',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Arkansas','AR',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('California','CA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Colorado','CO',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Connecticut','CT',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Delaware','DE',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('District of Columbia','DC',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Florida','FL',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Georgia','GA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Guam','GU',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Hawaii','HI',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Idaho','ID',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Illinois','IL',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Indiana','IN',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Iowa','IA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Kansas','KS',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Kentucky','KY',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Louisiana','LA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Maine','ME',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Maryland','MD',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Massachusetts','MA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Michigan','MI',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Minnesota','MN',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Mississippi','MS',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Missouri','MO',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Montana','MT',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Nebraska','NE',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Nevada','NV',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('New Hampshire','NH',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('New Jersey','NJ',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('New Mexico','NM',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('New York','NY',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('North Carolina','NC',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('North Dakota','ND',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Ohio','OH',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Oklahoma','OK',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Oregon','OR',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Pennsylvania','PA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Puerto Rico','PR',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Rhode Island','RI',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('South Carolina','SC',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('South Dakota','SD',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Tennessee','TN',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Texas','TX',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Utah','UT',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Vermont','VT',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Virginia','VA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Virgin Islands','VI',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Washington','WA',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('West Virginia','WV',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Wisconsin','WI',1)
INSERT INTO State([Name], Abbr, Country_ID) VALUES('Wyoming','WY',1)

GO

CREATE PROC InsertCustomer(
	@FName  NVARCHAR(20),
	@LName NVARCHAR(30),
	@Address_ID INT,
	@Email NVARCHAR(200) )
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO Customer
	(FName, LName, Address_ID, email)
	VALUES
	(@FName, @LName, @Address_ID, @Email)
	
	RETURN SCOPE_IDENTITY()
END

GO

CREATE PROC UpdateCustomer(
	@ID INT,
	@FName  NVARCHAR(20),
	@LName NVARCHAR(30),
	@Address_ID INT,
	@Email NVARCHAR(200) )
AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Customer
	SET FName = @FName,
	LName = @LName,
	Address_ID = @Address_ID,
	Email = @Email
	WHERE ID = @ID
END

GO

CREATE PROC InsertAddress(
	@Street NVARCHAR(200),
	@City NVARCHAR(50),
	@State_ID INT,
	@PostalCode NVARCHAR(15) )
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO Address
	(Street, City, State_ID, PostalCode)
	VALUES
	(@Street, @City, @State_ID, @PostalCode)
	
	RETURN SCOPE_IDENTITY()
END

GO

CREATE PROC UpdateAddress(
	@ID INT,
	@Street NVARCHAR(200),
	@City NVARCHAR(50),
	@State_ID INT,
	@PostalCode NVARCHAR(15) )
AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Address
	SET Street = @Street,
	City = @City,
	State_ID = @State_ID,
	PostalCode = @PostalCode
	WHERE ID = @ID
	
END

GO

CREATE PROC InsertPhone(
	@Number NVARCHAR(15),
	@Type_ID INT)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO Phone
	(Number, Type_ID)
	VALUES
	(@Number, @Type_ID)
	
	RETURN SCOPE_IDENTITY()
	
END

GO

CREATE PROC InsertCustomerPhone(
	@Customer_ID INT,
	@Phone_ID INT)
AS
BEGIN
	SET NOCOUNT ON
	
	IF( NOT EXISTS( SELECT * FROM Customer_Phone
		WHERE Customer_ID = @Customer_ID AND Phone_ID = @Phone_ID) )
	BEGIN
		INSERT INTO Customer_Phone
		(Customer_ID, Phone_ID)
		VALUES
		(@Customer_ID, @Phone_ID)
	END
	
END

GO

CREATE PROC GetCustomer
AS
BEGIN
	SET NOCOUNT ON

	SELECT ID, Fname, LName, Email, Address_ID
	FROM Customer
	
END

GO

CREATE PROC GetFullCustomer
AS
BEGIN
	SET NOCOUNT ON

	SELECT Customer.ID, Fname, LName, Email,
		A.ID AS Address_ID, A.Street, A.City, S.ID AS State_ID, S.Name AS State, 
		C.ID AS Country_ID, C.Name AS Country, A.PostalCode
	FROM Customer 	
	JOIN Address A ON A.ID = Address_ID
	JOIN State S ON S.ID = A.State_ID
	JOIN Country C ON C.ID = S.Country_ID
	
END

GO

CREATE PROC GetPhonesForCustomer(@CustomerID INT)
AS
BEGIN
	SET NOCOUNT ON

	SELECT Phone_ID, Number, PT.ID AS [Type_ID], PT.Name
	FROM Customer_Phone
	JOIN Phone P ON P.ID = Phone_ID
	JOIN PhoneType PT ON PT.ID = P.[Type_ID]
	WHERE Customer_ID = @CustomerID
END

GO
CREATE PROC Clear
AS

BEGIN

DELETE Customer_Phone
DELETE Phone
DELETE Customer
DELETE Address

END