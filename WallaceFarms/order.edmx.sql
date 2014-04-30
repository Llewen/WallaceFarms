
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/29/2014 20:45:27
-- Generated from EDMX file: C:\Users\Simon\Documents\GitHub\WallaceFarms\WallaceFarms\order.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OrderBeef];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_butcherID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BeefOrder] DROP CONSTRAINT [fk_butcherID];
GO
IF OBJECT_ID(N'[dbo].[fk_orderID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BeefOrder] DROP CONSTRAINT [fk_orderID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[BeefOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BeefOrder];
GO
IF OBJECT_ID(N'[dbo].[Butcher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Butcher];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(255)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'BeefOrders'
CREATE TABLE [dbo].[BeefOrders] (
    [BeefOrderID] int  NOT NULL,
    [OrderID] int  NULL,
    [ButcherID] int  NULL,
    [NumQuarters] int  NULL,
    [Comments] varchar(160)  NULL
);
GO

-- Creating table 'Butchers'
CREATE TABLE [dbo].[Butchers] (
    [ButcherID] int  NOT NULL,
    [Name] varchar(80)  NULL,
    [Phone] varchar(10)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderID] int  NOT NULL,
    [Name] varchar(80)  NULL,
    [Phone] varchar(10)  NULL,
    [Email] varchar(80)  NULL,
    [Status] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId] ASC);
GO

-- Creating primary key on [BeefOrderID] in table 'BeefOrders'
ALTER TABLE [dbo].[BeefOrders]
ADD CONSTRAINT [PK_BeefOrders]
    PRIMARY KEY CLUSTERED ([BeefOrderID] ASC);
GO

-- Creating primary key on [ButcherID] in table 'Butchers'
ALTER TABLE [dbo].[Butchers]
ADD CONSTRAINT [PK_Butchers]
    PRIMARY KEY CLUSTERED ([ButcherID] ASC);
GO

-- Creating primary key on [OrderID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ButcherID] in table 'BeefOrders'
ALTER TABLE [dbo].[BeefOrders]
ADD CONSTRAINT [fk_butcherID]
    FOREIGN KEY ([ButcherID])
    REFERENCES [dbo].[Butchers]
        ([ButcherID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_butcherID'
CREATE INDEX [IX_fk_butcherID]
ON [dbo].[BeefOrders]
    ([ButcherID]);
GO

-- Creating foreign key on [OrderID] in table 'BeefOrders'
ALTER TABLE [dbo].[BeefOrders]
ADD CONSTRAINT [fk_orderID]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[Orders]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'fk_orderID'
CREATE INDEX [IX_fk_orderID]
ON [dbo].[BeefOrders]
    ([OrderID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------