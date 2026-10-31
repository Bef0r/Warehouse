-- =========================
-- WAREHOUSES
-- =========================

INSERT INTO "Warehouses" ("Name", "Address")
VALUES
    ('Central Warehouse', 'Budapest, Industrial Park 1'),
    ('West Warehouse', 'Veszprém, Factory Road 12');


-- =========================
-- CABINETS
-- =========================

INSERT INTO "Cabinets" ("Name", "WarehouseId")
VALUES
    ('CAB-A01', (SELECT "Id" FROM "Warehouses" WHERE "Name" = 'Central Warehouse')),
    ('CAB-A02', (SELECT "Id" FROM "Warehouses" WHERE "Name" = 'Central Warehouse')),
    ('CAB-B01', (SELECT "Id" FROM "Warehouses" WHERE "Name" = 'West Warehouse')),
    ('CAB-B02', (SELECT "Id" FROM "Warehouses" WHERE "Name" = 'West Warehouse'));


-- =========================
-- SHELVES
-- =========================

INSERT INTO "Shelves"
    ("Name", "CabinetId", "Width", "Height", "Depth")
VALUES
    ('Shelf-01', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-A01'), 1200, 800, 600),
    ('Shelf-02', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-A01'), 1200, 800, 600),

    ('Shelf-01', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-A02'), 1500, 1000, 700),
    ('Shelf-02', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-A02'), 1500, 1000, 700),

    ('Shelf-01', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-B01'), 1000, 700, 500),
    ('Shelf-02', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-B01'), 1000, 700, 500),

    ('Shelf-01', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-B02'), 2000, 1200, 800),
    ('Shelf-02', (SELECT "Id" FROM "Cabinets" WHERE "Name" = 'CAB-B02'), 2000, 1200, 800);


-- =========================
-- CATEGORIES
-- =========================

INSERT INTO "Categories" ("Name")
VALUES
    ('Electronics'),
    ('Mechanical Parts'),
    ('Sensors'),
    ('Cables');


-- =========================
-- PRODUCTS
-- =========================

INSERT INTO "Products"
    ("CategoryId", "SKU", "Name", "Width", "Height", "Depth", "MinimumStock")
VALUES
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Electronics'),
        'ELEC-001',
        'Control Module',
        100, 80, 150, 5
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Electronics'),
        'ELEC-002',
        'Power Supply',
        120, 100, 180, 3
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Mechanical Parts'),
        'MECH-001',
        'Mounting Bracket',
        200, 100, 150, 10
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Mechanical Parts'),
        'MECH-002',
        'Aluminium Plate',
        300, 20, 200, 5
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Sensors'),
        'SENS-001',
        'Photoelectric Sensor',
        50, 50, 100, 8
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Sensors'),
        'SENS-002',
        'Distance Sensor',
        80, 70, 120, 4
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Cables'),
        'CAB-001',
        'Ethernet Cable 2m',
        30, 30, 2000, 20
    ),
    (
        (SELECT "Id" FROM "Categories" WHERE "Name" = 'Cables'),
        'CAB-002',
        'Power Cable 3m',
        40, 40, 3000, 15
    );


-- =========================
-- STOCK
-- =========================

INSERT INTO "Stocks"
    ("ShelfId", "ProductId", "Quantity", "PositionX", "PositionY", "PositionZ")
VALUES

-- Central Warehouse / CAB-A01 / Shelf-01
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'ELEC-001'),

    10, 0, 0, 0
),

(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'SENS-001'),

    15, 300, 0, 0
),

-- Central Warehouse / CAB-A01 / Shelf-02
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-02'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'MECH-001'),

    5, 0, 0, 0
),

-- Central Warehouse / CAB-A02 / Shelf-01
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-A02' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'ELEC-002'),

    8, 0, 0, 0
),

(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-A02' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'SENS-002'),

    6, 500, 0, 0
),

-- Central Warehouse / CAB-A02 / Shelf-02
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-A02' AND s."Name" = 'Shelf-02'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'CAB-001'),

    12, 0, 0, 0
),

-- West Warehouse / CAB-B01 / Shelf-01
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-B01' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'MECH-002'),

    4, 0, 0, 0
),

-- West Warehouse / CAB-B01 / Shelf-02
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-B01' AND s."Name" = 'Shelf-02'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'CAB-002'),

    10, 0, 0, 0
),

-- West Warehouse / CAB-B02 / Shelf-01
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-B02' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'ELEC-001'),

    7, 0, 0, 0
),

(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-B02' AND s."Name" = 'Shelf-01'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'SENS-001'),

    10, 500, 0, 0
),

-- West Warehouse / CAB-B02 / Shelf-02
(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-B02' AND s."Name" = 'Shelf-02'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'MECH-001'),

    8, 0, 0, 0
),

(
    (SELECT s."Id"
     FROM "Shelves" s
     JOIN "Cabinets" c ON c."Id" = s."CabinetId"
     WHERE c."Name" = 'CAB-B02' AND s."Name" = 'Shelf-02'),

    (SELECT "Id" FROM "Products" WHERE "SKU" = 'CAB-002'),

    5, 500, 0, 0
);


-- =========================
-- STOCK MOVEMENTS
-- MovementType:
-- 0 = Incoming
-- 1 = Outgoing
-- 2 = Transfer
-- =========================

INSERT INTO "StockMovements"
    ("ProductId", "FromShelfId", "ToShelfId", "Quantity",
     "MovementType", "CreatedAt", "Note")
VALUES

-- Incoming
(
    (SELECT "Id" FROM "Products" WHERE "SKU" = 'ELEC-001'),
    NULL,
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-01'
    ),
    10,
    0,
    NOW() - INTERVAL '5 days',
    'Initial stock delivery'
),

-- Incoming
(
    (SELECT "Id" FROM "Products" WHERE "SKU" = 'SENS-001'),
    NULL,
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-01'
    ),
    15,
    0,
    NOW() - INTERVAL '4 days',
    'Sensor delivery'
),

-- Transfer
(
    (SELECT "Id" FROM "Products" WHERE "SKU" = 'ELEC-001'),
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-01'
    ),
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-B02' AND s."Name" = 'Shelf-01'
    ),
    7,
    2,
    NOW() - INTERVAL '3 days',
    'Transfer to West Warehouse'
),

-- Outgoing
(
    (SELECT "Id" FROM "Products" WHERE "SKU" = 'MECH-001'),
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-A01' AND s."Name" = 'Shelf-02'
    ),
    NULL,
    2,
    1,
    NOW() - INTERVAL '2 days',
    'Production usage'
),

-- Incoming
(
    (SELECT "Id" FROM "Products" WHERE "SKU" = 'CAB-001'),
    NULL,
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-A02' AND s."Name" = 'Shelf-02'
    ),
    12,
    0,
    NOW() - INTERVAL '1 day',
    'Cable delivery'
),

-- Transfer
(
    (SELECT "Id" FROM "Products" WHERE "SKU" = 'CAB-002'),
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-B02' AND s."Name" = 'Shelf-02'
    ),
    (
        SELECT s."Id"
        FROM "Shelves" s
        JOIN "Cabinets" c ON c."Id" = s."CabinetId"
        WHERE c."Name" = 'CAB-B01' AND s."Name" = 'Shelf-02'
    ),
    5,
    2,
    NOW(),
    'Shelf reorganization'
);