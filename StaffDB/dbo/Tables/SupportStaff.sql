CREATE TABLE [dbo].[SupportStaff] (
    [staff_id] INT NOT NULL,
    [age]      INT NOT NULL,
    CONSTRAINT [PK_SupportStaff] PRIMARY KEY CLUSTERED ([staff_id] ASC),
    CONSTRAINT [FK_SupportStaff_staff] FOREIGN KEY ([staff_id]) REFERENCES [dbo].[Staff] ([staff_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

