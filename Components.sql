---------------------------------------------------------------------------
-- Заполнение таблицы Components и Specifications
---------------------------------------------------------------------------
-- Предполагаем: Stock = 10 для всех компонентов.

------------------------------------
-- 1. CPUs (CategoryID = 1)
------------------------------------
-- CPU 1: AMD Ryzen 5 5600X
DECLARE @ComponentID1 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Ryzen 5 5600X',
   N'Ядра: 6; Потоки: 12; Частота: 3.7-4.6 Ггц; TDP: 65W; Сокет: AM4',
   683.64, 10, 'AMD', 'Images\cpu\ryzen5600x.png', 1);
SET @ComponentID1 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID1, N'Ядра: 6'),
  (@ComponentID1, N'Потоки: 12'),
  (@ComponentID1, N'Частота: 3.7-4.6 Ггц'),
  (@ComponentID1, N'TDP: 65W'),
  (@ComponentID1, N'Сокет: AM4');

-- CPU 2: Intel Core i5-12400F
DECLARE @ComponentID2 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Intel Core i5-12400F',
   N'Ядра: 6; Потоки: 12; Частота: 2.5-4.4 Ггц; TDP: 65W; Сокет: LGA1700',
   575.64, 10, 'Intel', 'Images\cpu\12400f.png', 1);
SET @ComponentID2 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID2, N'Ядра: 6'),
  (@ComponentID2, N'Потоки: 12'),
  (@ComponentID2, N'Частота: 2.5-4.4 Ггц'),
  (@ComponentID2, N'TDP: 65W'),
  (@ComponentID2, N'Сокет: LGA1700');

-- CPU 3: AMD Ryzen 7 5800X
DECLARE @ComponentID3 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Ryzen 7 5800X',
   N'Ядра: 8; Потоки: 16; Частота: 3.8-4.7 Ггц; TDP: 105W; Сокет: AM4',
   899.64, 10, 'AMD', 'Images\cpu\5800x.png', 1);
SET @ComponentID3 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID3, N'Ядра: 8'),
  (@ComponentID3, N'Потоки: 16'),
  (@ComponentID3, N'Частота: 3.8-4.7 Ггц'),
  (@ComponentID3, N'TDP: 105W'),
  (@ComponentID3, N'Сокет: AM4');

-- CPU 4: Intel Core i7-12700K
DECLARE @ComponentID4 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Intel Core i7-12700K',
   N'Ядра: 12; Потоки: 20; Частота: 3.6-5.0 Ггц; TDP: 125W; Сокет: LGA1700',
   1043.64, 10, 'Intel', 'Images\cpu\12700k.png', 1);
SET @ComponentID4 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID4, N'Ядра: 12'),
  (@ComponentID4, N'Потоки: 20'),
  (@ComponentID4, N'Частота: 3.6-5.0 Ггц'),
  (@ComponentID4, N'TDP: 125W'),
  (@ComponentID4, N'Сокет: LGA1700');

-- CPU 5: AMD Ryzen 9 5950X
DECLARE @ComponentID5 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Ryzen 9 5950X',
   N'Ядра: 16; Потоки: 32; Частота: 3.4-4.9 Ггц; TDP: 105W; Сокет: AM4',
   1619.64, 10, 'AMD', 'Images\cpu\5900x.png', 1);
SET @ComponentID5 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID5, N'Ядра: 16'),
  (@ComponentID5, N'Потоки: 32'),
  (@ComponentID5, N'Частота: 3.4-4.9 Ггц'),
  (@ComponentID5, N'TDP: 105W'),
  (@ComponentID5, N'Сокет: AM4');

-- CPU 6: Intel Core i9-12900K
DECLARE @ComponentID6 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Intel Core i9-12900K',
   N'Ядра: 16; Потоки: 24; Частота: 3.2-5.2 Ггц; TDP: 125W; Сокет: LGA1700',
   1799.64, 10, 'Intel', 'Images\cpu\12900k.png', 1);
SET @ComponentID6 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID6, N'Ядра: 16'),
  (@ComponentID6, N'Потоки: 24'),
  (@ComponentID6, N'Частота: 3.2-5.2 Ггц'),
  (@ComponentID6, N'TDP: 125W'),
  (@ComponentID6, N'Сокет: LGA1700');

-- CPU 7: AMD Ryzen 5 3600
DECLARE @ComponentID7 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Ryzen 5 3600',
   N'Ядра: 6; Потоки: 12; Частота: 3.6-4.2 Ггц; TDP: 65W; Сокет: AM4',
   467.64, 10, 'AMD', 'Images\cpu\3600.png', 1);
SET @ComponentID7 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID7, N'Ядра: 6'),
  (@ComponentID7, N'Потоки: 12'),
  (@ComponentID7, N'Частота: 3.6-4.2 Ггц'),
  (@ComponentID7, N'TDP: 65W'),
  (@ComponentID7, N'Сокет: AM4');

-- CPU 8: Intel Core i3-12100F
DECLARE @ComponentID8 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Intel Core i3-12100F',
   N'Ядра: 4; Потоки: 8; Частота: 3.3-4.3 Ггц; TDP: 58W; Сокет: LGA1700',
   323.64, 10, 'Intel', 'Images\cpu\121000f.png', 1);
SET @ComponentID8 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID8, N'Ядра: 4'),
  (@ComponentID8, N'Потоки: 8'),
  (@ComponentID8, N'Частота: 3.3-4.3 Ггц'),
  (@ComponentID8, N'TDP: 58W'),
  (@ComponentID8, N'Сокет: LGA1700');

-- CPU 9: AMD Ryzen 9 5900X
DECLARE @ComponentID9 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Ryzen 9 5900X',
   N'Ядра: 12; Потоки: 24; Частота: 3.7-4.8 Ггц; TDP: 105W; Сокет: AM4',
   1367.64, 10, 'AMD', 'Images\cpu\5900x.png', 1);
SET @ComponentID9 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID9, N'Ядра: 12'),
  (@ComponentID9, N'Потоки: 24'),
  (@ComponentID9, N'Частота: 3.7-4.8 Ггц'),
  (@ComponentID9, N'TDP: 105W'),
  (@ComponentID9, N'Сокет: AM4');

-- CPU 10: Intel Core i5-12600K
DECLARE @ComponentID10 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Intel Core i5-12600K',
   N'Ядра: 10; Потоки: 16; Частота: 3.7-4.9 Ггц; TDP: 125W; Сокет: LGA1700',
   827.64, 10, 'Intel', 'Images\cpu\12400f.png', 1);
SET @ComponentID10 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID10, N'Ядра: 10'),
  (@ComponentID10, N'Потоки: 16'),
  (@ComponentID10, N'Частота: 3.7-4.9 Ггц'),
  (@ComponentID10, N'TDP: 125W'),
  (@ComponentID10, N'Сокет: LGA1700');

------------------------------------
-- 2. GPUs (CategoryID = 2)
------------------------------------
-- GPU 1: NVIDIA GeForce RTX 3060
DECLARE @ComponentID11 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NVIDIA GeForce RTX 3060',
   N'12GB GDDR6; 3584 ядер; Частота: 1320-1777 МГц; TDP: 170W',
   1439.64, 10, 'NVIDIA', 'Images\gpu\rtx3060.png', 2);
SET @ComponentID11 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID11, N'12GB GDDR6'),
  (@ComponentID11, N'3584 ядер'),
  (@ComponentID11, N'Частота: 1320-1777 МГц'),
  (@ComponentID11, N'TDP: 170W');

-- GPU 2: AMD Radeon RX 6600 XT
DECLARE @ComponentID12 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Radeon RX 6600 XT',
   N'8GB GDDR6; 2048 ядер; Частота: 1968-2589 МГц; TDP: 160W',
   1547.64, 10, 'AMD', 'Images\gpu\6600xt.png', 2);
SET @ComponentID12 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID12, N'8GB GDDR6'),
  (@ComponentID12, N'2048 ядер'),
  (@ComponentID12, N'Частота: 1968-2589 МГц'),
  (@ComponentID12, N'TDP: 160W');

-- GPU 3: NVIDIA GeForce RTX 3070
DECLARE @ComponentID13 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NVIDIA GeForce RTX 3070',
   N'8GB GDDR6; 5888 ядер; Частота: 1500-1725 МГц; TDP: 220W',
   2159.64, 10, 'NVIDIA', 'Images\gpu\rtx3080.png', 2);
SET @ComponentID13 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID13, N'8GB GDDR6'),
  (@ComponentID13, N'5888 ядер'),
  (@ComponentID13, N'Частота: 1500-1725 МГц'),
  (@ComponentID13, N'TDP: 220W');

-- GPU 4: AMD Radeon RX 6700 XT
DECLARE @ComponentID14 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Radeon RX 6700 XT',
   N'12GB GDDR6; 2560 ядер; Частота: 2424-2581 МГц; TDP: 230W',
   1979.64, 10, 'AMD', 'Images\gpu\6700xt.png', 2);
SET @ComponentID14 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID14, N'12GB GDDR6'),
  (@ComponentID14, N'2560 ядер'),
  (@ComponentID14, N'Частота: 2424-2581 МГц'),
  (@ComponentID14, N'TDP: 230W');

-- GPU 5: NVIDIA GeForce RTX 3080
DECLARE @ComponentID15 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NVIDIA GeForce RTX 3080',
   N'10GB GDDR6X; 8704 ядер; Частота: 1440-1710 МГц; TDP: 320W',
   2879.64, 10, 'NVIDIA', 'Images\gpu\rtx3080.png', 2);
SET @ComponentID15 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID15, N'10GB GDDR6X'),
  (@ComponentID15, N'8704 ядер'),
  (@ComponentID15, N'Частота: 1440-1710 МГц'),
  (@ComponentID15, N'TDP: 320W');

-- GPU 6: AMD Radeon RX 6800
DECLARE @ComponentID16 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Radeon RX 6800',
   N'16GB GDDR6; 3840 ядер; Частота: 1815-2105 МГц; TDP: 250W',
   2519.64, 10, 'AMD', 'Images\gpu\rtx3080.png', 2);
SET @ComponentID16 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID16, N'16GB GDDR6'),
  (@ComponentID16, N'3840 ядер'),
  (@ComponentID16, N'Частота: 1815-2105 МГц'),
  (@ComponentID16, N'TDP: 250W');

-- GPU 7: NVIDIA GeForce RTX 3090
DECLARE @ComponentID17 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NVIDIA GeForce RTX 3090',
   N'24GB GDDR6X; 10496 ядер; Частота: 1395-1695 МГц; TDP: 350W',
   4679.64, 10, 'NVIDIA', 'Images\gpu\RTX3090.png', 2);
SET @ComponentID17 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID17, N'24GB GDDR6X'),
  (@ComponentID17, N'10496 ядер'),
  (@ComponentID17, N'Частота: 1395-1695 МГц'),
  (@ComponentID17, N'TDP: 350W');

-- GPU 8: AMD Radeon RX 6900 XT
DECLARE @ComponentID18 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Radeon RX 6900 XT',
   N'16GB GDDR6; 5120 ядер; Частота: 2015-2250 МГц; TDP: 300W',
   3239.64, 10, 'AMD', 'Images\gpu\rx6900xt.png', 2);
SET @ComponentID18 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID18, N'16GB GDDR6'),
  (@ComponentID18, N'5120 ядер'),
  (@ComponentID18, N'Частота: 2015-2250 МГц'),
  (@ComponentID18, N'TDP: 300W');

-- GPU 9: NVIDIA GeForce RTX 3050
DECLARE @ComponentID19 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NVIDIA GeForce RTX 3050',
   N'8GB GDDR6; 2560 ядер; Частота: 1552-1777 МГц; TDP: 130W',
   1079.64, 10, 'NVIDIA', 'Images\gpu\rtx3060.png', 2);
SET @ComponentID19 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID19, N'8GB GDDR6'),
  (@ComponentID19, N'2560 ядер'),
  (@ComponentID19, N'Частота: 1552-1777 МГц'),
  (@ComponentID19, N'TDP: 130W');

-- GPU 10: AMD Radeon RX 6500 XT
DECLARE @ComponentID20 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'AMD Radeon RX 6500 XT',
   N'4GB GDDR6; 1024 ядер; Частота: 2610-2815 МГц; TDP: 107W',
   899.64, 10, 'AMD', 'Images\gpu\6500xt.png', 2);
SET @ComponentID20 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID20, N'4GB GDDR6'),
  (@ComponentID20, N'1024 ядер'),
  (@ComponentID20, N'Частота: 2610-2815 МГц'),
  (@ComponentID20, N'TDP: 107W');

------------------------------------
-- 3. RAMs (CategoryID = 3)
------------------------------------
-- RAM 1: Corsair Vengeance LPX 16GB
DECLARE @ComponentID21 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Corsair Vengeance LPX 16GB',
   N'DDR4 3200MHz; CL16; 2x8GB; Напряжение: 1.35V',
   215.64, 10, 'Corsair', 'Images\ram\Corsair Vengeance LPX 16GB.png', 3);
SET @ComponentID21 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID21, N'DDR4 3200MHz'),
  (@ComponentID21, N'CL16'),
  (@ComponentID21, N'2x8GB'),
  (@ComponentID21, N'Напряжение: 1.35V');

-- RAM 2: Kingston Fury Beast 32GB
DECLARE @ComponentID22 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Kingston Fury Beast 32GB',
   N'DDR4 3600MHz; CL18; 2x16GB; Напряжение: 1.35V',
   395.64, 10, 'Kingston', 'Images\ram\Kingston Fury Beast 32GB.png', 3);
SET @ComponentID22 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID22, N'DDR4 3600MHz'),
  (@ComponentID22, N'CL18'),
  (@ComponentID22, N'2x16GB'),
  (@ComponentID22, N'Напряжение: 1.35V');

-- RAM 3: G.Skill Trident Z RGB 32GB
DECLARE @ComponentID23 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'G.Skill Trident Z RGB 32GB',
   N'DDR4 3600MHz; CL16; 2x16GB; Напряжение: 1.35V',
   467.64, 10, 'G.Skill', 'Images\ram\G.Skill Trident Z RGB 32GB.png', 3);
SET @ComponentID23 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID23, N'DDR4 3600MHz'),
  (@ComponentID23, N'CL16'),
  (@ComponentID23, N'2x16GB'),
  (@ComponentID23, N'Напряжение: 1.35V');

-- RAM 4: Crucial Ballistix 16GB
DECLARE @ComponentID24 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Crucial Ballistix 16GB',
   N'DDR4 3200MHz; CL16; 2x8GB; Напряжение: 1.35V',
   197.64, 10, 'Crucial', 'Images\ram\Crucial Ballistix 16GB.png', 3);
SET @ComponentID24 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID24, N'DDR4 3200MHz'),
  (@ComponentID24, N'CL16'),
  (@ComponentID24, N'2x8GB'),
  (@ComponentID24, N'Напряжение: 1.35V');

-- RAM 5: HyperX Predator 32GB
DECLARE @ComponentID25 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'HyperX Predator 32GB',
   N'DDR4 3200MHz; CL16; 2x16GB; Напряжение: 1.35V',
   431.64, 10, 'HyperX', 'Images\ram\HyperX Predator 32GB.png', 3);
SET @ComponentID25 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID25, N'DDR4 3200MHz'),
  (@ComponentID25, N'CL16'),
  (@ComponentID25, N'2x16GB'),
  (@ComponentID25, N'Напряжение: 1.35V');

-- RAM 6: Patriot Viper Steel 16GB
DECLARE @ComponentID26 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Patriot Viper Steel 16GB',
   N'DDR4 3200MHz; CL16; 2x8GB; Напряжение: 1.35V',
   179.64, 10, 'Patriot', 'Images\ram\Patriot Viper Steel 16GB.png', 3);
SET @ComponentID26 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID26, N'DDR4 3200MHz'),
  (@ComponentID26, N'CL16'),
  (@ComponentID26, N'2x8GB'),
  (@ComponentID26, N'Напряжение: 1.35V');

-- RAM 7: Team Group T-Force Delta RGB 32GB
DECLARE @ComponentID27 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Team Group T-Force Delta RGB 32GB',
   N'DDR4 3600MHz; CL18; 2x16GB; Напряжение: 1.35V',
   413.64, 10, 'Team Group', 'Images\ram\eam Group T-Force Delta RGB 32GB.png', 3);
SET @ComponentID27 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID27, N'DDR4 3600MHz'),
  (@ComponentID27, N'CL18'),
  (@ComponentID27, N'2x16GB'),
  (@ComponentID27, N'Напряжение: 1.35V');

-- RAM 8: ADATA XPG Spectrix D50 16GB
DECLARE @ComponentID28 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ADATA XPG Spectrix D50 16GB',
   N'DDR4 3600MHz; CL18; 2x8GB; Напряжение: 1.35V',
   233.64, 10, 'ADATA', 'Images\ram\ADATA XPG Spectrix D50 16GB.png', 3);
SET @ComponentID28 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID28, N'DDR4 3600MHz'),
  (@ComponentID28, N'CL18'),
  (@ComponentID28, N'2x8GB'),
  (@ComponentID28, N'Напряжение: 1.35V');

-- RAM 9: G.Skill Ripjaws V 64GB
DECLARE @ComponentID29 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'G.Skill Ripjaws V 64GB',
   N'DDR4 3200MHz; CL16; 2x32GB; Напряжение: 1.35V',
   683.64, 10, 'G.Skill', 'Images\ram\G.Skill Ripjaws V 64GB.png', 3);
SET @ComponentID29 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID29, N'DDR4 3200MHz'),
  (@ComponentID29, N'CL16'),
  (@ComponentID29, N'2x32GB'),
  (@ComponentID29, N'Напряжение: 1.35V');

-- RAM 10: Kingston ValueRAM 8GB
DECLARE @ComponentID30 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Kingston ValueRAM 8GB',
   N'DDR4 2666MHz; CL19; 1x8GB; Напряжение: 1.2V',
   107.64, 10, 'Kingston', 'Images\ram\Kingston ValueRAM 8GB.png', 3);
SET @ComponentID30 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID30, N'DDR4 2666MHz'),
  (@ComponentID30, N'CL19'),
  (@ComponentID30, N'1x8GB'),
  (@ComponentID30, N'Напряжение: 1.2V');

  ------------------------------------
-- 4. Motherboards (CategoryID = 4)
------------------------------------
-- MB 1: ASUS ROG Strix B550-F
DECLARE @ComponentID31 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ASUS ROG Strix B550-F',
   N'Socket AM4; AMD B550; 4 слота DDR4; Форм‑фактор: ATX',
   539.64, 10, 'ASUS', 'Images\motherboard\ASUS ROG Strix B550-F.png', 4);
SET @ComponentID31 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID31, N'Socket AM4'),
  (@ComponentID31, N'AMD B550'),
  (@ComponentID31, N'4 слота DDR4'),
  (@ComponentID31, N'Форм‑фактор: ATX');

-- MB 2: MSI MAG B660 Tomahawk
DECLARE @ComponentID32 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'MSI MAG B660 Tomahawk',
   N'Socket LGA1700; Intel B660; 4 слота DDR4; Форм‑фактор: ATX',
   611.64, 10, 'MSI', 'Images\motherboard\MSI MAG B660 Tomahawk.png', 4);
SET @ComponentID32 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID32, N'Socket LGA1700'),
  (@ComponentID32, N'Intel B660'),
  (@ComponentID32, N'4 слота DDR4'),
  (@ComponentID32, N'Форм‑фактор: ATX');

-- MB 3: Gigabyte X570 Aorus Elite
DECLARE @ComponentID33 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Gigabyte X570 Aorus Elite',
   N'Socket AM4; AMD X570; 4 слота DDR4; Форм‑фактор: ATX',
   683.64, 10, 'Gigabyte', 'Images\motherboard\Gigabyte X570 Aorus Elite.png', 4);
SET @ComponentID33 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID33, N'Socket AM4'),
  (@ComponentID33, N'AMD X570'),
  (@ComponentID33, N'4 слота DDR4'),
  (@ComponentID33, N'Форм‑фактор: ATX');

-- MB 4: ASRock Z690 Phantom Gaming
DECLARE @ComponentID34 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ASRock Z690 Phantom Gaming',
   N'Socket LGA1700; Intel Z690; 4 слота DDR4; Форм‑фактор: ATX',
   791.64, 10, 'ASRock', 'Images\motherboard\ASRock Z690 Phantom Gaming.png', 4);
SET @ComponentID34 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID34, N'Socket LGA1700'),
  (@ComponentID34, N'Intel Z690'),
  (@ComponentID34, N'4 слота DDR4'),
  (@ComponentID34, N'Форм‑фактор: ATX');

-- MB 5: ASUS TUF Gaming B450-Plus
DECLARE @ComponentID35 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ASUS TUF Gaming B450-Plus',
   N'Socket AM4; AMD B450; 4 слота DDR4; Форм‑фактор: ATX',
   359.64, 10, 'ASUS', 'Images\motherboard\ASUS TUF Gaming B450-Plus.png', 4);
SET @ComponentID35 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID35, N'Socket AM4'),
  (@ComponentID35, N'AMD B450'),
  (@ComponentID35, N'4 слота DDR4'),
  (@ComponentID35, N'Форм‑фактор: ATX');

-- MB 6: MSI MPG B550 Gaming Plus
DECLARE @ComponentID36 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'MSI MPG B550 Gaming Plus',
   N'Socket AM4; AMD B550; 4 слота DDR4; Форм‑фактор: ATX',
   467.64, 10, 'MSI', 'Images\motherboard\MSI MPG B550 Gaming Plus.png', 4);
SET @ComponentID36 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID36, N'Socket AM4'),
  (@ComponentID36, N'AMD B550'),
  (@ComponentID36, N'4 слота DDR4'),
  (@ComponentID36, N'Форм‑фактор: ATX');

-- MB 7: Gigabyte B660M DS3H
DECLARE @ComponentID37 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Gigabyte B660M DS3H',
   N'Socket LGA1700; Intel B660; 4 слота DDR4; Форм‑фактор: mATX',
   395.64, 10, 'Gigabyte', 'Images\motherboard\Gigabyte B660M DS3H.png', 4);
SET @ComponentID37 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID37, N'Socket LGA1700'),
  (@ComponentID37, N'Intel B660'),
  (@ComponentID37, N'4 слота DDR4'),
  (@ComponentID37, N'Форм‑фактор: mATX');

-- MB 8: ASUS Prime H510M-K
DECLARE @ComponentID38 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ASUS Prime H510M-K',
   N'Socket LGA1200; Intel H510; 2 слота DDR4; Форм‑фактор: mATX',
   287.64, 10, 'ASUS', 'Images\motherboard\ASUS Prime H510M-K.png', 4);
SET @ComponentID38 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID38, N'Socket LGA1200'),
  (@ComponentID38, N'Intel H510'),
  (@ComponentID38, N'2 слота DDR4'),
  (@ComponentID38, N'Форм‑фактор: mATX');

-- MB 9: Biostar B450MH
DECLARE @ComponentID39 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Biostar B450MH',
   N'Socket AM4; AMD B450; 2 слота DDR4; Форм‑фактор: mATX',
   215.64, 10, 'Biostar', 'Images\motherboard\Biostar B450MH.png', 4);
SET @ComponentID39 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID39, N'Socket AM4'),
  (@ComponentID39, N'AMD B450'),
  (@ComponentID39, N'2 слота DDR4'),
  (@ComponentID39, N'Форм‑фактор: mATX');

-- MB 10: ASRock A520M-HDV
DECLARE @ComponentID40 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ASRock A520M-HDV',
   N'Socket AM4; AMD A520; 2 слота DDR4; Форм‑фактор: mATX',
   197.64, 10, 'ASRock', 'Images\motherboard\ASRock A520M-HDV.png', 4);
SET @ComponentID40 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID40, N'Socket AM4'),
  (@ComponentID40, N'AMD A520'),
  (@ComponentID40, N'2 слота DDR4'),
  (@ComponentID40, N'Форм‑фактор: mATX');

------------------------------------
-- 5. PSUs (CategoryID = 5)
------------------------------------
-- PSU 1: Corsair RM750x
DECLARE @ComponentID41 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Corsair RM750x',
   N'750W; 80+ Gold; Полномодульный; Вентилятор: 135mm',
   395.64, 10, 'Corsair', 'Images\psu\Corsair RM750x.png', 5);
SET @ComponentID41 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID41, N'750W'),
  (@ComponentID41, N'80+ Gold'),
  (@ComponentID41, N'Полномодульный'),
  (@ComponentID41, N'Вентилятор: 135mm');

-- PSU 2: be quiet! Straight Power 11 850W
DECLARE @ComponentID42 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'be quiet! Straight Power 11 850W',
   N'850W; 80+ Platinum; Полномодульный; Вентилятор: 135mm',
   467.64, 10, 'be quiet!', 'Images\psu\be quiet! Straight Power 11 850W.png', 5);
SET @ComponentID42 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID42, N'850W'),
  (@ComponentID42, N'80+ Platinum'),
  (@ComponentID42, N'Полномодульный'),
  (@ComponentID42, N'Вентилятор: 135mm');

-- PSU 3: Seasonic Focus GX-650
DECLARE @ComponentID43 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Seasonic Focus GX-650',
   N'650W; 80+ Gold; Полномодульный; Вентилятор: 120mm',
   323.64, 10, 'Seasonic', 'Images\psu\Seasonic Focus GX-650.png', 5);
SET @ComponentID43 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID43, N'650W'),
  (@ComponentID43, N'80+ Gold'),
  (@ComponentID43, N'Полномодульный'),
  (@ComponentID43, N'Вентилятор: 120mm');

-- PSU 4: EVGA SuperNOVA 850 G6
DECLARE @ComponentID44 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'EVGA SuperNOVA 850 G6',
   N'850W; 80+ Gold; Полномодульный; Вентилятор: 140mm',
   431.64, 10, 'EVGA', 'Images\psu\EVGA SuperNOVA 850 G6.png', 5);
SET @ComponentID44 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID44, N'850W'),
  (@ComponentID44, N'80+ Gold'),
  (@ComponentID44, N'Полномодульный'),
  (@ComponentID44, N'Вентилятор: 140mm');

-- PSU 5: Thermaltake Toughpower GF1 750W
DECLARE @ComponentID45 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Thermaltake Toughpower GF1 750W',
   N'750W; 80+ Gold; Полномодульный; Вентилятор: 140mm',
   359.64, 10, 'Thermaltake', 'Images\psu\Thermaltake Toughpower GF1 750W.png', 5);
SET @ComponentID45 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID45, N'750W'),
  (@ComponentID45, N'80+ Gold'),
  (@ComponentID45, N'Полномодульный'),
  (@ComponentID45, N'Вентилятор: 140mm');

-- PSU 6: Cooler Master MWE Gold 550
DECLARE @ComponentID46 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Cooler Master MWE Gold 550',
   N'550W; 80+ Gold; Не модульный; Вентилятор: 120mm',
   215.64, 10, 'Cooler Master', 'Images\psu\Cooler Master MWE Gold 550.png', 5);
SET @ComponentID46 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID46, N'550W'),
  (@ComponentID46, N'80+ Gold'),
  (@ComponentID46, N'Не модульный'),
  (@ComponentID46, N'Вентилятор: 120mm');

-- PSU 7: FSP Hydro G Pro 1000W
DECLARE @ComponentID47 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'FSP Hydro G Pro 1000W',
   N'1000W; 80+ Gold; Полномодульный; Вентилятор: 140mm',
   539.64, 10, 'FSP', 'Images\psu\FSP Hydro G Pro 1000W.png', 5);
SET @ComponentID47 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID47, N'1000W'),
  (@ComponentID47, N'80+ Gold'),
  (@ComponentID47, N'Полномодульный'),
  (@ComponentID47, N'Вентилятор: 140mm');

  ------------------------------------
-- 6. Storages (CategoryID = 6)
------------------------------------
-- Storage 1: Samsung 980 Pro 1TB
DECLARE @ComponentID48 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Samsung 980 Pro 1TB',
   N'NVMe PCIe 4.0; 1TB; 7000/5000 МБ/с; Ресурс: 600TBW',
   467.64, 10, 'Samsung', 'Images\storage\Samsung 980 Pro 1TB.png', 6);
SET @ComponentID48 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID48, N'NVMe PCIe 4.0'),
  (@ComponentID48, N'1TB'),
  (@ComponentID48, N'7000/5000 МБ/с'),
  (@ComponentID48, N'Ресурс: 600TBW');

-- Storage 2: WD Blue SN570 1TB
DECLARE @ComponentID49 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'WD Blue SN570 1TB',
   N'NVMe PCIe 3.0; 1TB; 3500/3000 МБ/с; Ресурс: 600TBW',
   323.64, 10, 'WD', 'Images\storage\WD Blue SN570 1TB.png', 6);
SET @ComponentID49 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID49, N'NVMe PCIe 3.0'),
  (@ComponentID49, N'1TB'),
  (@ComponentID49, N'3500/3000 МБ/с'),
  (@ComponentID49, N'Ресурс: 600TBW');

-- Storage 3: Crucial P5 Plus 1TB
DECLARE @ComponentID50 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Crucial P5 Plus 1TB',
   N'NVMe PCIe 4.0; 1TB; 6600/5000 МБ/с; Ресурс: 600TBW',
   395.64, 10, 'Crucial', 'Images\storage\Crucial P5 Plus 1TB.png', 6);
SET @ComponentID50 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID50, N'NVMe PCIe 4.0'),
  (@ComponentID50, N'1TB'),
  (@ComponentID50, N'6600/5000 МБ/с'),
  (@ComponentID50, N'Ресурс: 600TBW');

-- Storage 4: Seagate FireCuda 530 1TB
DECLARE @ComponentID51 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Seagate FireCuda 530 1TB',
   N'NVMe PCIe 4.0; 1TB; 7300/6000 МБ/с; Ресурс: 1275TBW',
   503.64, 10, 'Seagate', 'Images\storage\Seagate FireCuda 530 1TB.png', 6);
SET @ComponentID51 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID51, N'NVMe PCIe 4.0'),
  (@ComponentID51, N'1TB'),
  (@ComponentID51, N'7300/6000 МБ/с'),
  (@ComponentID51, N'Ресурс: 1275TBW');

-- Storage 5: Kingston KC3000 1TB
DECLARE @ComponentID52 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Kingston KC3000 1TB',
   N'NVMe PCIe 4.0; 1TB; 7000/6000 МБ/с; Ресурс: 800TBW',
   431.64, 10, 'Kingston', 'Images\storage\Kingston KC3000 1TB.png', 6);
SET @ComponentID52 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID52, N'NVMe PCIe 4.0'),
  (@ComponentID52, N'1TB'),
  (@ComponentID52, N'7000/6000 МБ/с'),
  (@ComponentID52, N'Ресурс: 800TBW');

-- Storage 6: Samsung 870 EVO 1TB
DECLARE @ComponentID53 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Samsung 870 EVO 1TB',
   N'SATA III; 1TB; 560/530 МБ/с; Ресурс: 600TBW',
   359.64, 10, 'Samsung', 'Images\storage\Samsung 870 EVO 1TB.png', 6);
SET @ComponentID53 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID53, N'SATA III'),
  (@ComponentID53, N'1TB'),
  (@ComponentID53, N'560/530 МБ/с'),
  (@ComponentID53, N'Ресурс: 600TBW');

-- Storage 7: WD Black SN770 1TB
DECLARE @ComponentID54 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'WD Black SN770 1TB',
   N'NVMe PCIe 4.0; 1TB; 5150/4900 МБ/с; Ресурс: 600TBW',
   395.64, 10, 'WD', 'Images\storage\WD Black SN770 1TB.png', 6);
SET @ComponentID54 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID54, N'NVMe PCIe 4.0'),
  (@ComponentID54, N'1TB'),
  (@ComponentID54, N'5150/4900 МБ/с'),
  (@ComponentID54, N'Ресурс: 600TBW');

-- Storage 8: Crucial MX500 1TB
DECLARE @ComponentID55 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Crucial MX500 1TB',
   N'SATA III; 1TB; 560/510 МБ/с; Ресурс: 360TBW',
   287.64, 10, 'Crucial', 'Images\storage\Crucial MX500 1TB.png', 6);
SET @ComponentID55 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID55, N'SATA III'),
  (@ComponentID55, N'1TB'),
  (@ComponentID55, N'560/510 МБ/с'),
  (@ComponentID55, N'Ресурс: 360TBW');

-- Storage 9: Seagate BarraCuda 2TB
DECLARE @ComponentID56 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Seagate BarraCuda 2TB',
   N'HDD 3.5"; 2TB; 7200 RPM; Кэш: 256MB',
   215.64, 10, 'Seagate', 'Images\storage\Seagate BarraCuda 2TB.png', 6);
SET @ComponentID56 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID56, N'HDD 3.5"'),
  (@ComponentID56, N'2TB'),
  (@ComponentID56, N'7200 RPM'),
  (@ComponentID56, N'Кэш: 256MB');

-- Storage 10: Toshiba P300 3TB
DECLARE @ComponentID57 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Toshiba P300 3TB',
   N'HDD 3.5"; 3TB; 5400 RPM; Кэш: 128MB',
   251.64, 10, 'Toshiba', 'Images\storage\Toshiba P300 3TB.png', 6);
SET @ComponentID57 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID57, N'HDD 3.5"'),
  (@ComponentID57, N'3TB'),
  (@ComponentID57, N'5400 RPM'),
  (@ComponentID57, N'Кэш: 128MB');

------------------------------------
-- 7. Coolers (CategoryID = 7)
------------------------------------
-- Cooler 1: Noctua NH-D15
DECLARE @ComponentID58 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Noctua NH-D15',
   N'Базовый; 2 вентилятора; TDP: 220W; Высота: 165mm',
   323.64, 10, 'Noctua', 'Images\Cooler\Noctua NH-D15.png', 7);
SET @ComponentID58 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID58, N'Базовый'),
  (@ComponentID58, N'2 вентилятора'),
  (@ComponentID58, N'TDP: 220W'),
  (@ComponentID58, N'Высота: 165mm');

-- Cooler 2: Cooler Master Hyper 212
DECLARE @ComponentID59 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Cooler Master Hyper 212',
   N'Базовый; 1 вентилятор; TDP: 150W; Высота: 159mm',
   125.64, 10, 'Cooler Master', 'Images\Cooler\Cooler Master Hyper 212.png', 7);
SET @ComponentID59 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID59, N'Базовый'),
  (@ComponentID59, N'1 вентилятор'),
  (@ComponentID59, N'TDP: 150W'),
  (@ComponentID59, N'Высота: 159mm');

-- Cooler 3: be quiet! Dark Rock Pro 4
DECLARE @ComponentID60 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'be quiet! Dark Rock Pro 4',
   N'Базовый; 2 вентилятора; TDP: 250W; Высота: 163mm',
   287.64, 10, 'be quiet!', 'Images\Cooler\be quiet! Dark Rock Pro 4.png', 7);
SET @ComponentID60 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID60, N'Базовый'),
  (@ComponentID60, N'2 вентилятора'),
  (@ComponentID60, N'TDP: 250W'),
  (@ComponentID60, N'Высота: 163mm');

-- Cooler 4: Deepcool AS500 Plus
DECLARE @ComponentID61 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Deepcool AS500 Plus',
   N'Базовый; 2 вентилятора; TDP: 220W; Высота: 164mm',
   179.64, 10, 'Deepcool', 'Images\Cooler\Deepcool AS500 Plus.png', 7);
SET @ComponentID61 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID61, N'Базовый'),
  (@ComponentID61, N'2 вентилятора'),
  (@ComponentID61, N'TDP: 220W'),
  (@ComponentID61, N'Высота: 164mm');

-- Cooler 5: Arctic Freezer 34 eSports
DECLARE @ComponentID62 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Arctic Freezer 34 eSports',
   N'Базовый; 1 вентилятор; TDP: 210W; Высота: 157mm',
   143.64, 10, 'Arctic', 'Images\Cooler\Arctic Freezer 34 eSports.png', 7);
SET @ComponentID62 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID62, N'Базовый'),
  (@ComponentID62, N'1 вентилятор'),
  (@ComponentID62, N'TDP: 210W'),
  (@ComponentID62, N'Высота: 157mm');

-- Cooler 6: Scythe Fuma 2
DECLARE @ComponentID63 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Scythe Fuma 2',
   N'Базовый; 2 вентилятора; TDP: 200W; Высота: 155mm',
   215.64, 10, 'Scythe', 'Images\Cooler\Scythe Fuma 2.png', 7);
SET @ComponentID63 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID63, N'Базовый'),
  (@ComponentID63, N'2 вентилятора'),
  (@ComponentID63, N'TDP: 200W'),
  (@ComponentID63, N'Высота: 155mm');

-- Cooler 7: Thermalright Peerless Assassin 120
DECLARE @ComponentID64 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Thermalright Peerless Assassin 120',
   N'Базовый; 2 вентилятора; TDP: 240W; Высота: 157mm',
   197.64, 10, 'Thermalright', 'Images\Cooler\Thermalright Peerless Assassin 120.png', 7);
SET @ComponentID64 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID64, N'Базовый'),
  (@ComponentID64, N'2 вентилятора'),
  (@ComponentID64, N'TDP: 240W'),
  (@ComponentID64, N'Высота: 157mm');

-- Cooler 8: ID-COOLING SE-224-XT
DECLARE @ComponentID65 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'ID-COOLING SE-224-XT',
   N'Базовый; 1 вентилятор; TDP: 180W; Высота: 154mm',
   89.64, 10, 'ID-COOLING', 'Images\Cooler\ID-COOLING SE-224-XT.png', 7);
SET @ComponentID65 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID65, N'Базовый'),
  (@ComponentID65, N'1 вентилятор'),
  (@ComponentID65, N'TDP: 180W'),
  (@ComponentID65, N'Высота: 154mm');

-- Cooler 9: NZXT Kraken X63
DECLARE @ComponentID66 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NZXT Kraken X63',
   N'СЖО 280mm; 2 вентилятора; TDP: 300W; Радиатор: 280mm',
   431.64, 10, 'NZXT', 'Images\Cooler\NZXT Kraken X63.png', 7);
SET @ComponentID66 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID66, N'СЖО 280mm'),
  (@ComponentID66, N'2 вентилятора'),
  (@ComponentID66, N'TDP: 300W'),
  (@ComponentID66, N'Радиатор: 280mm');

-- Cooler 10: Corsair iCUE H100i RGB
DECLARE @ComponentID67 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Corsair iCUE H100i RGB',
   N'СЖО 240mm; 2 вентилятора; TDP: 275W; Радиатор: 240mm',
   395.64, 10, 'Corsair', 'Images\Cooler\Corsair iCUE H100i RGB.png', 7);
SET @ComponentID67 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID67, N'СЖО 240mm'),
  (@ComponentID67, N'2 вентилятора'),
  (@ComponentID67, N'TDP: 275W'),
  (@ComponentID67, N'Радиатор: 240mm');

------------------------------------
-- 8. Cases (CategoryID = 8)
------------------------------------
-- Case 1: NZXT H510
DECLARE @ComponentID68 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'NZXT H510',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 2x120mm',
   287.64, 10, 'NZXT', 'Images\case\NZXT H510.png', 8);
SET @ComponentID68 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID68, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID68, N'Цвет: черный'),
  (@ComponentID68, N'Окно: закаленное стекло'),
  (@ComponentID68, N'Вентиляторы: 2x120mm');

-- Case 2: Fractal Design Meshify C
DECLARE @ComponentID69 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Fractal Design Meshify C',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 2x120mm',
   323.64, 10, 'Fractal Design', 'Images\case\Fractal Design Meshify C.png', 8);
SET @ComponentID69 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID69, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID69, N'Цвет: черный'),
  (@ComponentID69, N'Окно: закаленное стекло'),
  (@ComponentID69, N'Вентиляторы: 2x120mm');

-- Case 3: Lian Li PC-O11 Dynamic
DECLARE @ComponentID70 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Lian Li PC-O11 Dynamic',
   N'Форм‑фактор: Full‑Tower; Цвет: белый; Окно: закаленное стекло; Вентиляторы: нет',
   539.64, 10, 'Lian Li', 'Images\case\Lian Li PC-O11 Dynamic.png', 8);
SET @ComponentID70 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID70, N'Форм‑фактор: Full‑Tower'),
  (@ComponentID70, N'Цвет: белый'),
  (@ComponentID70, N'Окно: закаленное стекло'),
  (@ComponentID70, N'Вентиляторы: нет');

-- Case 4: Corsair 4000D Airflow
DECLARE @ComponentID71 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Corsair 4000D Airflow',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 2x120mm',
   359.64, 10, 'Corsair', 'Images\case\Corsair 4000D Airflow.png', 8);
SET @ComponentID71 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID71, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID71, N'Цвет: черный'),
  (@ComponentID71, N'Окно: закаленное стекло'),
  (@ComponentID71, N'Вентиляторы: 2x120mm');

-- Case 5: Cooler Master MasterBox MB520
DECLARE @ComponentID72 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Cooler Master MasterBox MB520',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 3x120mm',
   251.64, 10, 'Cooler Master', 'Images\case\Cooler Master MasterBox MB520.png', 8);
SET @ComponentID72 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID72, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID72, N'Цвет: черный'),
  (@ComponentID72, N'Окно: закаленное стекло'),
  (@ComponentID72, N'Вентиляторы: 3x120mm');

-- Case 6: Phanteks Eclipse P400A
DECLARE @ComponentID73 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Phanteks Eclipse P400A',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 3x120mm',
   305.64, 10, 'Phanteks', 'Images\case\Phanteks Eclipse P400A.png', 8);
SET @ComponentID73 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID73, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID73, N'Цвет: черный'),
  (@ComponentID73, N'Окно: закаленное стекло'),
  (@ComponentID73, N'Вентиляторы: 3x120mm');

-- Case 7: be quiet! Pure Base 500DX
DECLARE @ComponentID74 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'be quiet! Pure Base 500DX',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 3x140mm',
   395.64, 10, 'be quiet!', 'Images\case\be quiet! Pure Base 500DX.png', 8);
SET @ComponentID74 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID74, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID74, N'Цвет: черный'),
  (@ComponentID74, N'Окно: закаленное стекло'),
  (@ComponentID74, N'Вентиляторы: 3x140mm');

-- Case 8: Thermaltake Core P3
DECLARE @ComponentID75 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Thermaltake Core P3',
   N'Форм‑фактор: Open Frame; Цвет: черный; Окно: закаленное стекло; Вентиляторы: нет',
   431.64, 10, 'Thermaltake', 'Images\case\Thermaltake Core P3.png', 8);
SET @ComponentID75 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID75, N'Форм‑фактор: Open Frame'),
  (@ComponentID75, N'Цвет: черный'),
  (@ComponentID75, N'Окно: закаленное стекло'),
  (@ComponentID75, N'Вентиляторы: нет');

-- Case 9: Deepcool MATREXX 55
DECLARE @ComponentID76 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Deepcool MATREXX 55',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: закаленное стекло; Вентиляторы: 1x120mm',
   215.64, 10, 'Deepcool', 'Images\case\Deepcool MATREXX 55.png', 8);
SET @ComponentID76 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID76, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID76, N'Цвет: черный'),
  (@ComponentID76, N'Окно: закаленное стекло'),
  (@ComponentID76, N'Вентиляторы: 1x120mm');

-- Case 10: Aerocool Cylon
DECLARE @ComponentID77 INT;
INSERT INTO dbo.Components
  (Name, Description, Price, Stock, Manufacturer, ImagePath, CategoryID)
VALUES
  (N'Aerocool Cylon',
   N'Форм‑фактор: Mid‑Tower; Цвет: черный; Окно: акрил; Вентиляторы: 1x120mm',
   179.64, 10, 'Aerocool', 'Images\case\Aerocool Cylon.png', 8);
SET @ComponentID77 = SCOPE_IDENTITY();
INSERT INTO dbo.Specifications (ComponentID, SpecText)
VALUES
  (@ComponentID77, N'Форм‑фактор: Mid‑Tower'),
  (@ComponentID77, N'Цвет: черный'),
  (@ComponentID77, N'Окно: акрил'),
  (@ComponentID77, N'Вентиляторы: 1x120mm');
