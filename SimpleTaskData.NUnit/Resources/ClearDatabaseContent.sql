DELETE FROM [Task]
DBCC CHECKIDENT ('Task', reseed, 1)


