CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "produtos" (
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "codigo" VARCHAR(50) NOT NULL UNIQUE,        
    "descricao" VARCHAR(255) NOT NULL,           
    "saldo" INT NOT NULL DEFAULT 0,               
    "criado_em" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "versao" INT DEFAULT 1 NOT NULL
);

INSERT INTO "produtos" ("codigo", "descricao", "saldo", "versao") VALUES
('PROD-001', 'Monitor Gamer 24 Pol', 15, 1),
('PROD-002', 'Teclado Mecânico RGB', 25, 1),
('PROD-003', 'Mouse Óptico 10000 DPI', 40, 1),
('PROD-004', 'Headset 7.1 Surround', 12, 1),
('PROD-005', 'Cadeira Escritório Ergonômica', 8, 1),
('PROD-006', 'Webcam Full HD 1080p', 20, 1),
('PROD-007', 'SSD NVMe 1TB', 50, 1),
('PROD-008', 'Memória RAM 16GB DDR4', 35, 1),
('PROD-009', 'Placa de Vídeo RTX 3060', 5, 1),
('PROD-010', 'Processador Core i7 12ª Gen', 10, 1)
ON CONFLICT (id) DO NOTHING;;