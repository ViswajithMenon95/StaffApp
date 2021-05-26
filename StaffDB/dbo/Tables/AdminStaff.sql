CREATE TABLE [dbo].[AdminStaff] (
    [staff_id]   INT          NOT NULL,
    [department] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AdminStaff] PRIMARY KEY CLUSTERED ([staff_id] ASC),
    CONSTRAINT [FK_AdminStaff_staff] FOREIGN KEY ([staff_id]) REFERENCES [dbo].[Staff] ([staff_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

