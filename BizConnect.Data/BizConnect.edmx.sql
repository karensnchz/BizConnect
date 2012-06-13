
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/19/2012 21:06:17
-- Generated from EDMX file: C:\Users\ksanchez\Documents\Neumont\Quarter 7\PRO390\MobileEnabledMvcApp\BizConnect.Data\BizConnect.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BizConnect];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoices] DROP CONSTRAINT [FK_ClientInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyLayout]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Layouts] DROP CONSTRAINT [FK_CompanyLayout];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyClient_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyClient] DROP CONSTRAINT [FK_CompanyClient_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyClient_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyClient] DROP CONSTRAINT [FK_CompanyClient_Company];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceInvoiceData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoiceDatas] DROP CONSTRAINT [FK_InvoiceInvoiceData];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[InvoiceDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoiceDatas];
GO
IF OBJECT_ID(N'[dbo].[Invoices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoices];
GO
IF OBJECT_ID(N'[dbo].[Layouts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Layouts];
GO
IF OBJECT_ID(N'[dbo].[CompanyClient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyClient];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Telephone] nchar(10)  NOT NULL,
    [Cell_Phone] nchar(10)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Birthday] datetime  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Telephone] nchar(10)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'InvoiceDatas'
CREATE TABLE [dbo].[InvoiceDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Labor] decimal(19,2)  NOT NULL,
    [Tax] decimal(19,2)  NOT NULL,
    [Total] decimal(19,2)  NOT NULL,
    [Invoice_Id] int  NOT NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Payment_Status] bit  NOT NULL,
    [Total] int  NOT NULL,
    [Client_Id] int  NOT NULL
);
GO

-- Creating table 'Layouts'
CREATE TABLE [dbo].[Layouts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Elements] varbinary(max)  NOT NULL,
    [Company_Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CompanyClient'
CREATE TABLE [dbo].[CompanyClient] (
    [Clients_Id] int  NOT NULL,
    [Companies_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InvoiceDatas'
ALTER TABLE [dbo].[InvoiceDatas]
ADD CONSTRAINT [PK_InvoiceDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Layouts'
ALTER TABLE [dbo].[Layouts]
ADD CONSTRAINT [PK_Layouts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Clients_Id], [Companies_Id] in table 'CompanyClient'
ALTER TABLE [dbo].[CompanyClient]
ADD CONSTRAINT [PK_CompanyClient]
    PRIMARY KEY NONCLUSTERED ([Clients_Id], [Companies_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Client_Id] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [FK_ClientInvoice]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientInvoice'
CREATE INDEX [IX_FK_ClientInvoice]
ON [dbo].[Invoices]
    ([Client_Id]);
GO

-- Creating foreign key on [Company_Id] in table 'Layouts'
ALTER TABLE [dbo].[Layouts]
ADD CONSTRAINT [FK_CompanyLayout]
    FOREIGN KEY ([Company_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyLayout'
CREATE INDEX [IX_FK_CompanyLayout]
ON [dbo].[Layouts]
    ([Company_Id]);
GO

-- Creating foreign key on [Clients_Id] in table 'CompanyClient'
ALTER TABLE [dbo].[CompanyClient]
ADD CONSTRAINT [FK_CompanyClient_Client]
    FOREIGN KEY ([Clients_Id])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Companies_Id] in table 'CompanyClient'
ALTER TABLE [dbo].[CompanyClient]
ADD CONSTRAINT [FK_CompanyClient_Company]
    FOREIGN KEY ([Companies_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyClient_Company'
CREATE INDEX [IX_FK_CompanyClient_Company]
ON [dbo].[CompanyClient]
    ([Companies_Id]);
GO

-- Creating foreign key on [Invoice_Id] in table 'InvoiceDatas'
ALTER TABLE [dbo].[InvoiceDatas]
ADD CONSTRAINT [FK_InvoiceInvoiceData]
    FOREIGN KEY ([Invoice_Id])
    REFERENCES [dbo].[Invoices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceInvoiceData'
CREATE INDEX [IX_FK_InvoiceInvoiceData]
ON [dbo].[InvoiceDatas]
    ([Invoice_Id]);
GO


INSERT INTO Companies VALUES("BizConnect", "River Park", 8013000000, "bc@hmail.com")
INSERT INTO Clients VALUES("Karen Sanchez", "420 W Cadbury Dr",8012087255, NULL, "ks@gmail.com", "06/20/1993")
-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------