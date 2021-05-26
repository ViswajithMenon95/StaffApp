CREATE TABLE [dbo].[TeacherStaff] (
    [staff_id] INT          NOT NULL,
    [subject]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TeacherStaff] PRIMARY KEY CLUSTERED ([staff_id] ASC),
    CONSTRAINT [FK_TeacherStaff_staff] FOREIGN KEY ([staff_id]) REFERENCES [dbo].[Staff] ([staff_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

