# **Resumen de PostgreSQL**

### **Creación y Modificación de Tablas, Campos y Restricciones**

- **Creación de tablas:**

  ```sql
  CREATE TABLE nombre_tabla (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    edad INT CHECK (edad >= 0)
  );
  ```

- **Modificación de tablas:**

  ```sql
  ALTER TABLE nombre_tabla 
  ADD columna_nueva TEXT;

  ALTER TABLE nombre_tabla 
  DROP COLUMN columna_nueva;

  ALTER TABLE nombre_tabla 
  RENAME COLUMN columna_nueva TO nueva_columna;
  ```

- **Restricciones comunes:**

  - `PRIMARY KEY`: Identifica registros únicos.  
  - `FOREIGN KEY`: Relación con otra tabla.  
  - `UNIQUE`: Valores únicos.  
  - `NOT NULL`: Campo obligatorio.  
  - `CHECK`: Condición específica.

---

### **Manipulación de Datos**

- **INSERT:**

  ```sql
  INSERT INTO nombre_tabla (col1, col2) 
  VALUES ('valor1', 'valor2');
  ```

- **DELETE:**

  ```sql
  DELETE FROM nombre_tabla 
  WHERE condicion;
  ```

- **UPDATE:**

  ```sql
  UPDATE nombre_tabla 
  SET col1 = 'nuevo_valor' 
  WHERE condicion;
  ```

---

### **Consultas (SELECT)**

- **Básica:**

  ```sql
  SELECT * FROM nombre_tabla;
  ```

- **Filtrado (WHERE):**

  ```sql
  SELECT * FROM nombre_tabla 
  WHERE col1 = 'valor';
  ```

- **Ordenación (ORDER BY):**

  ```sql
  SELECT * FROM nombre_tabla 
  ORDER BY col1 DESC;
  ```

- **Agrupación (GROUP BY):**

  ```sql
  SELECT col1, COUNT(*) 
  FROM nombre_tabla 
  GROUP BY col1;
  ```

- **Joins:**

  ```sql
  SELECT t1.col1, t2.col2 
  FROM tabla1 t1 
  JOIN tabla2 t2 ON t1.id = t2.id;
  ```

---

### **Elementos Adicionales**

- **Índices:**

  ```sql
  CREATE INDEX idx_nombre 
  ON nombre_tabla (col1);
  ```

- **Secuencias:**

  ```sql
  CREATE SEQUENCE secuencia_ejemplo 
  START 1 INCREMENT 1;
  ```

- **Vistas:**

  ```sql
  CREATE VIEW vista_ejemplo AS 
  SELECT col1, col2 FROM nombre_tabla;
  ```

---

### **Transacciones**

- **Iniciar transacción:**

  ```sql
  BEGIN;
  ```

- **Confirmar cambios:**

  ```sql
  COMMIT;
  ```

- **Deshacer cambios:**

  ```sql
  ROLLBACK;
  ```

---

# **Cheat Sheet PostgreSQL**

| Comando                 | Descripción                  |
|------------------------|------------------------------|
| `CREATE TABLE`         | Crear tabla                 |
| `ALTER TABLE`          | Modificar tabla             |
| `DROP TABLE`           | Eliminar tabla              |
| `INSERT INTO`          | Insertar datos              |
| `DELETE FROM`          | Eliminar registros          |
| `UPDATE`               | Actualizar registros        |
| `SELECT`               | Consultar datos             |
| `JOIN`                 | Combinar tablas             |
| `CREATE INDEX`         | Crear índice                |
| `CREATE SEQUENCE`      | Crear secuencia             |
| `CREATE VIEW`          | Crear vista                 |
| `BEGIN/COMMIT/ROLLBACK`| Control de transacciones    |
