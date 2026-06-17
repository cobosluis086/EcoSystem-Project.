CREATE TABLE Categorias (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion TEXT,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Usuarios (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    rol VARCHAR(50) DEFAULT 'Estudiante',
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Productos (
    id SERIAL PRIMARY KEY,
    categoria_id INT NOT NULL,
    nombre VARCHAR(150) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10,2) NOT NULL
        CHECK (precio >= 0),
    stock INT NOT NULL DEFAULT 0
        CHECK (stock >= 0),
    sku VARCHAR(50) UNIQUE,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_categoria
    FOREIGN KEY (categoria_id)
    REFERENCES Categorias(id)
    ON DELETE CASCADE
);

-- =====================================
-- DATOS
-- =====================================

INSERT INTO Categorias (nombre, descripcion)
VALUES
(
    'Computadoras',
    'Equipos de computo y laptops'
),
(
    'Accesorios',
    'Perifericos y accesorios para computadora'
);

INSERT INTO Usuarios (nombre, email, password_hash, rol)
VALUES
(
    'María González',
    'maria.gonzalez@empresa.com',
    'AQAAAAIAAYagAAAAEG...',
    'Gerente de Ventas'
),
(
    'Carlos Ramírez',
    'carlos.ramirez@empresa.com',
    'AQAAAAIAAYagAAAAEG...',
    'Jefe de Inventario'
),
(
    'Ana Martínez',
    'ana.martinez@empresa.com',
    'AQAAAAIAAYagAAAAEG...',
    'Auxiliar Administrativo'
);

INSERT INTO Productos
(categoria_id, nombre, descripcion, precio, stock, sku)
VALUES
(
    1,
    'Laptop Dell XPS 15',
    'Laptop de alto rendimiento',
    28999.99,
    10,
    'DELL-XPS15'
),
(
    2,
    'Mouse Logitech MX Master',
    'Mouse inalambrico ergonomico',
    1599.00,
    50,
    'LOGI-MXM'
),
(
    2,
    'Teclado Mecanico Keychron',
    'Teclado mecanico para programacion',
    2499.50,
    30,
    'KEY-K2'
);

SELECT * FROM Categorias;
SELECT * FROM Usuarios;
SELECT * FROM Productos;