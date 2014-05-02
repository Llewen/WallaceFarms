CREATE TABLE [dbo].[Order] (
    [OrderID] INT          NOT NULL IDENTITY PRIMARY KEY,
    [Name]    VARCHAR (80) NULL,
    [Phone]   VARCHAR (10) NULL,
    [Email]   VARCHAR (80) NULL,
    [Status]  INT          NULL,
    CONSTRAINT [pk_order] PRIMARY KEY CLUSTERED ([OrderID] ASC)
);

