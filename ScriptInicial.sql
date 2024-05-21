USE [QuerUmLivro]
GO

INSERT INTO [dbo].[Usuario]([Nome],[Email],[Desativado]) VALUES ('Jorge','jorge@gmail.com', 0)
INSERT INTO [dbo].[Usuario]([Nome],[Email],[Desativado]) VALUES ('Daiany','daiany@gmail.com', 0)
INSERT INTO [dbo].[Usuario]([Nome],[Email],[Desativado]) VALUES ('Marieta','marieta@gmail.com', 0)
INSERT INTO [dbo].[Usuario]([Nome],[Email],[Desativado]) VALUES ('Melina','melina@gmail.com', 0)
INSERT INTO [dbo].[Usuario]([Nome],[Email],[Desativado]) VALUES ('Ravena','ravena@gmail.com', 0)

INSERT INTO [dbo].[Livro]([Nome],[DoadorId],[Disponivel])VALUES('Livro de ciências',1,1)
INSERT INTO [dbo].[Livro]([Nome],[DoadorId],[Disponivel])VALUES('Livro de matemática',1,1)
INSERT INTO [dbo].[Livro]([Nome],[DoadorId],[Disponivel])VALUES('Livro de português',1,1)
INSERT INTO [dbo].[Livro]([Nome],[DoadorId],[Disponivel])VALUES('Livro de história',2,1)

INSERT INTO [dbo].[Interesse]([LivroId],[InteressadoId],[Justificativa],[Status],[Aprovado],[Data])
							VALUES(1,3,'Gostaria de Ter o Livro', 0, 0, GETDATE())
INSERT INTO [dbo].[Interesse]([LivroId],[InteressadoId],[Justificativa],[Status],[Aprovado],[Data])
							VALUES(1,4,'Quero o livro para mim', 0, 0, GETDATE())
GO




