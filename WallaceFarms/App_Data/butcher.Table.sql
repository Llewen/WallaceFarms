CREATE TABLE [dbo].[Butcher] (
    [ButcherID] INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (80) NULL,
    [Phone]     VARCHAR (10) NULL,
    CONSTRAINT [pk_butcher] PRIMARY KEY CLUSTERED ([ButcherID] ASC)
);
