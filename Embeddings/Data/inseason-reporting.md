---
title: "In-season Reporting"
date: 2024-02-23
geekdocCollapseSection: true
---

Son bases de datos Sql Azure. 

## ¿Cómo se guardan los backups?

El backup lo hacemos mediante un runbook que es invocado desde el workflow de la sincronización de de ROIvolution. 

![](/Cloud/Backups/politica-backups/images/workflow-in-season.png "workflow")

**Runbooks**(en autoaccount-analyticalways): 

- backup-roi-reporting-databases: Hace el backup, para ello expone webhook 
- Backup-Remove-Reporting-Old-Days: elimina backups antiguos (superado periodo de retención). Se ejecuta una vez al día

## Dónde se guardan los backups?

Se guardan en Azure Storage, en la storage account **staasqlreportbackupweu** de la suscripciómn **mirror**.

![](/Cloud/Backups/politica-backups/images/storage-in-season-reporting.png "Storage")

Inicialmente se guardan con el tier *hot*. 
Hay una policy en el storage para pasar los blobs a *cool* (optimización de coste) cuando pasan 24 horas.

Tiers: https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview 

Como no llegamos a los 90 días de retención, no podemos pasar al siguiente nivel (*cold*)

