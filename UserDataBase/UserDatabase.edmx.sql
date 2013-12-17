
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/03/2013 21:09:47
-- Generated from EDMX file: C:\Users\brk\Documents\visual studio 2012\Projects\NordnetApiPoc\UserDataBase\UserDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-SocialStockMarket-20131130180703];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Name] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Property2] nvarchar(max)  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StockSet'
CREATE TABLE [dbo].[StockSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ask] nvarchar(max)  NOT NULL,
    [Bid] nvarchar(max)  NOT NULL,
    [StockName] nvarchar(max)  NOT NULL,
    [Ticker] nvarchar(max)  NOT NULL,
    [Change] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FolderSet'
CREATE TABLE [dbo].[FolderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Invested] nvarchar(max)  NOT NULL,
    [Change] nvarchar(max)  NOT NULL,
    [User_Name] int  NOT NULL,
    [User_UserId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HistorySet'
CREATE TABLE [dbo].[HistorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [User_Name] int  NOT NULL,
    [User_UserId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StockSet_UserStock'
CREATE TABLE [dbo].[StockSet_UserStock] (
    [Investment] nvarchar(max)  NOT NULL,
    [LastestUpdate] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [Folder_Id] int  NOT NULL
);
GO

-- Creating table 'FolderSet_FolderHistory'
CREATE TABLE [dbo].[FolderSet_FolderHistory] (
    [Date] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [History_Id] int  NOT NULL
);
GO

-- Creating table 'StockSet_HistoryStock'
CREATE TABLE [dbo].[StockSet_HistoryStock] (
    [Date] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [FolderHistory_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Name], [UserId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Name], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'StockSet'
ALTER TABLE [dbo].[StockSet]
ADD CONSTRAINT [PK_StockSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FolderSet'
ALTER TABLE [dbo].[FolderSet]
ADD CONSTRAINT [PK_FolderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HistorySet'
ALTER TABLE [dbo].[HistorySet]
ADD CONSTRAINT [PK_HistorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StockSet_UserStock'
ALTER TABLE [dbo].[StockSet_UserStock]
ADD CONSTRAINT [PK_StockSet_UserStock]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FolderSet_FolderHistory'
ALTER TABLE [dbo].[FolderSet_FolderHistory]
ADD CONSTRAINT [PK_FolderSet_FolderHistory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StockSet_HistoryStock'
ALTER TABLE [dbo].[StockSet_HistoryStock]
ADD CONSTRAINT [PK_StockSet_HistoryStock]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Name], [User_UserId] in table 'FolderSet'
ALTER TABLE [dbo].[FolderSet]
ADD CONSTRAINT [FK_FolderUser]
    FOREIGN KEY ([User_Name], [User_UserId])
    REFERENCES [dbo].[UserSet]
        ([Name], [UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FolderUser'
CREATE INDEX [IX_FK_FolderUser]
ON [dbo].[FolderSet]
    ([User_Name], [User_UserId]);
GO

-- Creating foreign key on [Folder_Id] in table 'StockSet_UserStock'
ALTER TABLE [dbo].[StockSet_UserStock]
ADD CONSTRAINT [FK_FolderUserStock]
    FOREIGN KEY ([Folder_Id])
    REFERENCES [dbo].[FolderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FolderUserStock'
CREATE INDEX [IX_FK_FolderUserStock]
ON [dbo].[StockSet_UserStock]
    ([Folder_Id]);
GO

-- Creating foreign key on [User_Name], [User_UserId] in table 'HistorySet'
ALTER TABLE [dbo].[HistorySet]
ADD CONSTRAINT [FK_UserHistory]
    FOREIGN KEY ([User_Name], [User_UserId])
    REFERENCES [dbo].[UserSet]
        ([Name], [UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHistory'
CREATE INDEX [IX_FK_UserHistory]
ON [dbo].[HistorySet]
    ([User_Name], [User_UserId]);
GO

-- Creating foreign key on [History_Id] in table 'FolderSet_FolderHistory'
ALTER TABLE [dbo].[FolderSet_FolderHistory]
ADD CONSTRAINT [FK_HistoryFolderHistory]
    FOREIGN KEY ([History_Id])
    REFERENCES [dbo].[HistorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HistoryFolderHistory'
CREATE INDEX [IX_FK_HistoryFolderHistory]
ON [dbo].[FolderSet_FolderHistory]
    ([History_Id]);
GO

-- Creating foreign key on [FolderHistory_Id] in table 'StockSet_HistoryStock'
ALTER TABLE [dbo].[StockSet_HistoryStock]
ADD CONSTRAINT [FK_FolderHistoryHistoryStock]
    FOREIGN KEY ([FolderHistory_Id])
    REFERENCES [dbo].[FolderSet_FolderHistory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FolderHistoryHistoryStock'
CREATE INDEX [IX_FK_FolderHistoryHistoryStock]
ON [dbo].[StockSet_HistoryStock]
    ([FolderHistory_Id]);
GO

-- Creating foreign key on [Id] in table 'StockSet_UserStock'
ALTER TABLE [dbo].[StockSet_UserStock]
ADD CONSTRAINT [FK_UserStock_inherits_Stock]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[StockSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'FolderSet_FolderHistory'
ALTER TABLE [dbo].[FolderSet_FolderHistory]
ADD CONSTRAINT [FK_FolderHistory_inherits_Folder]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[FolderSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'StockSet_HistoryStock'
ALTER TABLE [dbo].[StockSet_HistoryStock]
ADD CONSTRAINT [FK_HistoryStock_inherits_UserStock]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[StockSet_UserStock]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------