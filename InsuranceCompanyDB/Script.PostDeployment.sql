IF NOT EXISTS (SELECT 1 FROM dbo.[Partner])
BEGIN
    INSERT INTO dbo.[Partner] (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender)
    VALUES 
        ('John', 'Doe', '123 Main St', '12345678901234567890', '12345678901', 1, GETUTCDATE(), 'john@example.com', 0, 'ABC1234567', 'M'),
        ('Jane', 'Doe', '456 Elm St', '09876543210987654321', NULL, 2, GETUTCDATE(), 'jane@example.com', 1, 'DEF9876543', 'F'),
        ('Bob', 'Smith', '789 Oak St', '11112222333344445555', '98765432109', 1, GETUTCDATE(), 'bob@example.com', 0, 'GHI0987654', 'M'),
        ('Alice', 'Johnson', '321 Pine St', '55556666777788889999', '45678901234', 2, GETUTCDATE(), 'alice@example.com', 1, 'JKL4567890', 'F'),
        ('Michael', 'Brown', '654 Cedar St', '00008888999900001111', NULL, 1, GETUTCDATE(), 'michael@example.com', 0, 'MNO1234567', 'M'),
        ('Emily', 'Williams', '987 Maple St', '22223333444455556666', '98765432101', 2, GETUTCDATE(), 'emily@example.com', 1, 'PQR2345678', 'F'),
        ('David', 'Miller', '741 Birch St', '44445555666677778888', '12345678912', 1, GETUTCDATE(), 'david@example.com', 0, 'STU3456789', 'M'),
        ('Sarah', 'Taylor', '852 Walnut St', '99990000111122223333', NULL, 2, GETUTCDATE(), 'sarah@example.com', 1, 'VWX4567890', 'F'),
        ('Ryan', 'Anderson', '963 Cherry St', '00334444555566667777', '98765432103', 1, GETUTCDATE(), 'ryan@example.com', 0, 'YZA5678901', 'M'),
        ('Jessica', 'Martinez', '147 Peach St', '66667777888899990000', '12345678923', 2, GETUTCDATE(), 'jessica@example.com', 1, 'BCD6789012', 'N');

    INSERT INTO dbo.[Policy] (PolicyNumber, PolicyAmount, PartnerId)
    VALUES 
        ('POL1000001', 1000.00, 1),
        ('POL1000002', 1500.50, 2),
        ('POL1000003', 2000.00, 3),
        ('POL1000004', 2500.75, 4),
        ('POL1000005', 3000.00, 5),
        ('POL1000006', 3500.25, 6),
        ('POL1000007', 4000.00, 7),
        ('POL1000008', 4500.50, 8),
        ('POL1000009', 5000.00, 9),
        ('POL1000010', 5500.75, 9),
        ('POL1000021', 5000.00, 1),
        ('POL1000022', 1500.50, 2),
        ('POL1000032', 6000.00, 3),
        ('POL1000033', 2500.75, 4),
        ('POL1000034', 3000.00, 5),
        ('POL1000035', 3500.25, 6),
        ('POL1000056', 4000.00, 7),
        ('POL1000088', 4500.50, 8),
        ('POL1000032', 5000.00, 9),
        ('POL1000042', 5500.75, 9);
END



