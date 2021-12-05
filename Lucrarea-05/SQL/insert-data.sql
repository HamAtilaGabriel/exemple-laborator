SET IDENTITY_INSERT [dbo].[Product] ON
INSERT INTO [dbo].[Product] (ProductId, Code, Price, Stock) VALUES (0, 1, 4.20, 50)
INSERT INTO [dbo].[Product] (ProductId, Code, Price, Stock) VALUES (1, 2, 9.99, 20)
INSERT INTO [dbo].[Product] (ProductId, Code, Price, Stock) VALUES (2, 3, 12.99, 10)
INSERT INTO [dbo].[Product] (ProductId, Code, Price, Stock) VALUES (3, 7, 42.0, 5)
INSERT INTO [dbo].[Product] (ProductId, Code, Price, Stock) VALUES (4, 9, 69.99, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF

SET IDENTITY_INSERT [dbo].[OrderLine] ON
INSERT INTO [dbo].[OrderLine] (OrderLineId, ProductId, Quantity, Price) VALUES (0, 1, 3, 9.99)
SET IDENTITY_INSERT [dbo].[OrderLine] OFF

SET IDENTITY_INSERT [dbo].[OrderHeader] ON
INSERT INTO [dbo].[OrderHeader] (OrderId, Address, Total) VALUES (0, 'Grove Street, No 1', 29.97)
SET IDENTITY_INSERT [dbo].[OrderHeader] OFF