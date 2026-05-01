CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "nota_fiscal" (
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "numero_sequencial" SERIAL UNIQUE,       
    "status" VARCHAR(10) NOT NULL DEFAULT 'Aberta' 
        CHECK ("status" IN ('Aberta', 'Fechada')),
    "data_emissao" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE "itens_nota_fiscal" (
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "nota_fiscal_id" UUID NOT NULL,
    "produto_id" UUID NOT NULL,                    
    "quantidade" INT NOT NULL,                
    CONSTRAINT "fk_nota" FOREIGN KEY ("nota_fiscal_id") 
        REFERENCES "notas_fiscais"("id") ON DELETE CASCADE
);