CREATE TABLE [dbo].[Employees] (
    [EmployeeId]  INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [PhoneNumber] NVARCHAR (MAX) NULL,
    [DateOfBirth] DATE           NOT NULL,
    [HireDate]    DATE           NOT NULL,
    [Position]    NVARCHAR (MAX) NULL,
    [Department]  NVARCHAR (MAX) NULL,
    [IsActive]    BIT            NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

