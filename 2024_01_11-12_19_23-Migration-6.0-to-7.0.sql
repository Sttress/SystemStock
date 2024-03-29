﻿START TRANSACTION;

ALTER TABLE "Product" ALTER COLUMN "Price" TYPE numeric;

CREATE TABLE "StoreProduct" (
    "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
    "ProductId" bigint NOT NULL,
    "StoreId" bigint NOT NULL,
    "Amount" bigint NOT NULL,
    CONSTRAINT "PK_StoreProduct" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_StoreProduct_Product_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Product" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_StoreProduct_Store_StoreId" FOREIGN KEY ("StoreId") REFERENCES "Store" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Product_CategoryId" ON "Product" ("CategoryId");

CREATE INDEX "IX_StoreProduct_ProductId" ON "StoreProduct" ("ProductId");

CREATE INDEX "IX_StoreProduct_StoreId" ON "StoreProduct" ("StoreId");

ALTER TABLE "Product" ADD CONSTRAINT "FK_Product_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240111151610_7.0', '7.0.14');

COMMIT;

