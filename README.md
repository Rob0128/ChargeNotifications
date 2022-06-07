# ChargeNotifications

Web api for generating daily charge pdfs to a specified folder


Setup
Db table 
Name: 'Charge'
Columns: [Id] INT
      ,[CustomerId] INT
      ,[Description] VARCHAR
      ,[ChargeDescription] VARCHAR
      ,[CostPence] INT
      ,[CostTotal] INT
      ,[ChargeDate] DATETIME
