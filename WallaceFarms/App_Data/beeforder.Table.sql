CREATE TABLE [dbo].[BeefOrder] (
    [BeefOrderID] INT NOT NULL IDENTITY,
    [OrderID]     INT NULL,
    [ButcherID]   INT NULL,
    [NumQuarters] INT NULL,
    CONSTRAINT [pk_beefOrder] PRIMARY KEY CLUSTERED ([BeefOrderID] ASC),
    CONSTRAINT [fk_butcherID] FOREIGN KEY ([ButcherID]) REFERENCES [dbo].[Butcher] ([ButcherID]),
    CONSTRAINT [fk_orderID] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([OrderID])
);

