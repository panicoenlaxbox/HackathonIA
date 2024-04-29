---
title: "In-season Parameters"
date: 2024-02-23
geekdocCollapseSection: true
---

Son bases de datos Sql Azure. 

## ¿Cómo se guardan los backups?




**Runbooks**(en autoaccount-analyticalways): 

- production-backup-sql-parameters: Hace el backup, para ello dispone de  una programación que lo ejecuta diariamente a las 05:00 (Central European Time)


## Dónde se guardan los backups?

Se guardan en Azure Storage, en la storage account **staasqlparambackupweu** de la suscripciómn **mirror**, por un tiempo idefinido.

![](/Cloud/Backups/politica-backups/images/storage-parameters.png "Storage")

Inicialmente se guardan con el tier *hot*. 
Hay una policy en el storage para pasar los blobs a *cool* (optimización de coste) cuando pasan 24 horas.

Tiers: https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview 

Como no llegamos a los 90 días de retención, no podemos pasar al siguiente nivel (*cold*)

