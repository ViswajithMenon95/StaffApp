CREATE TABLE [dbo].[Staff] (
    [staff_id] INT          IDENTITY (1, 1) NOT NULL,
    [name]     VARCHAR (50) NOT NULL,
    [phone]    VARCHAR (50) NOT NULL,
    [type]     TINYINT      NULL,
    CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED ([staff_id] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Staff]
    ON [dbo].[Staff]([phone] ASC);

