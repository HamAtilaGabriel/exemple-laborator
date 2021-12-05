CREATE TABLE [dbo].[Product](
    [ProductId] [int] IDENTITY(1,1) NOT NULL,
    [Code] [int] NOT NULL,
    [Price] [float] NOT NULL,
    [Stock] [int] NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED(
        [ProductId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) on [PRIMARY]
GO

CREATE TABLE [dbo].[OrderHeader](
    [OrderId] [int] IDENTITY(1,1) NOT NULL,
    [Address] [varchar](200) NOT NULL,
    [Total] [float] NOT NULL,
    CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED(
        [OrderId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) on [PRIMARY]
GO

CREATE TABLE [dbo].[OrderLine](
    [OrderLineId] [int] IDENTITY(1,1) NOT NULL,
    [ProductId] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    [Price] [float] NOT NULL,
    CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED(
        [OrderLineId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) on [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLine] WITH CHECK ADD CONSTRAINT [FK_Order_Lines_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_Order_Lines_Product]
GO