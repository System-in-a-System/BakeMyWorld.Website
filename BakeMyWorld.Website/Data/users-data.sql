USE [BakeMyWorld]
GO
INSERT [dbo].[AspNetUsers] ([Id], [Nickname], [Password], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Admin', N'123', N'Admin', N'ADMIN', N'admin@mail.com', N'ADMIN@MAIL.COM', 1, NULL, NULL, NULL, NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Nickname], [Password], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2', N'SimpleUser', N'456', N'SimpleUser', N'SIMPLEUSER', N'user@mail.com', N'USER@MAIL.COM', 1, NULL, NULL, NULL, NULL, 0, 0, NULL, 0, 0)
GO
