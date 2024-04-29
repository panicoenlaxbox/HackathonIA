---
title: "Sales Reporting"
date: 2024-02-23
geekdocCollapseSection: true
---

Son bases de datos Sql Azure. 

## ¿Cómo se guardan los backups?

El backup lo hacemos mediante un runbook que es invocado desde el workflow de la sincronización del módulo de ventas. Esto implica que se hacen varios backups al día

![](/Cloud/Backups/politica-backups/images/workflow-sales.png "workflow")

**Runbooks**(en autoaccount-analyticalways): 

- backup-roi-reporting-sales-databases: Hace el backup, para ello expone webhook 
- Backup-Remove-Sales-Old-Days: elimina backups antiguos (superado periodo de retención). Se ejecuta una vez al día


## Dónde se guardan los backups?

Se guardan en Azure Storage, en la storage account **staasqlrepsalesbackupweu** de la suscripciómn **mirror**

![](/Cloud/Backups/politica-backups/images/storage-sales.png "Storage")

Inicialmente se guardan con el tier *hot*. 
Hay una policy en el storage para pasar los blobs a *cool* (optimización de coste) cuando pasan 24 horas.

Tiers: https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview 

Como no llegamos a los 90 días de retención, no podemos pasar al siguiente nivel (*cold*)

## TODO

- **Parece que también se están haciendo backups de estas bases de datos de modo colateral**, al hacer backups de las bases de datos de reporting. Revisar